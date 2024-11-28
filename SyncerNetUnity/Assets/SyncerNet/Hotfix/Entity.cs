using MemoryPack;
using SyncerNet.Hotfix.Messages;
using System;
using System.Collections.Concurrent;
using UnityEngine;
using YooAsset;

#nullable enable
namespace SyncerNet.Hotfix
{
    [MemoryPackable]
    public partial class Entity
    {
        public uint EntityId { get; set; }
        public uint WorldId { get; set; }
        public uint OwnerId { get; set; }
        public string PrefabPath { get; set; }
        public ConcurrentDictionary<Type, Syncer> Syncers { get; private set; }

        [MemoryPackIgnore]
        public World World;
        [MemoryPackIgnore]
        public GameObject? GameObject { get; set; }
        [MemoryPackIgnore]
        public bool IsLocal => OwnerId == PlayerConfigs.PlayerId;
        [MemoryPackIgnore]
        public bool Initialized = false;

        [MemoryPackConstructor]
        private Entity(uint entityId, uint worldId, uint ownerId, string prefabPath, ConcurrentDictionary<Type, Syncer> syncers)
        {
            EntityId = entityId;
            WorldId = worldId;
            OwnerId = ownerId;
            PrefabPath = prefabPath;
            Syncers = syncers;
        }

        public Entity(uint entityId, uint ownerId, string prefabPath, World world)
        {
            EntityId = entityId;
            WorldId = world.WorldId;
            OwnerId = ownerId;
            PrefabPath = prefabPath;
            World = world;
            Syncers = new ConcurrentDictionary<Type, Syncer>();
        }

        public void Initialize()
        {
            if (Initialized) return;
            Debug.Log("Initialized");
            if (!string.IsNullOrEmpty(PrefabPath))
            {
                GameObject = YooAssets.GetPackage("DefaultPackage")
                .LoadAssetSync<GameObject>(PrefabPath)
                .InstantiateSync();
            }
            Initialized = true;
        }

        public void AddSyncer<T>() where T : Syncer, new()
        {
            Syncers.TryAdd(typeof(T), new T());
        }

        public void AddOrSetSyncer<T>(T tSyncer) where T : Syncer
        {
            if (!Syncers.TryAdd(typeof(T), tSyncer))
            {
                Syncers[typeof(T)] = tSyncer;
            }
        }

        public T? GetSyncer<T>() where T : Syncer
        {
            if (Syncers.TryGetValue(typeof(T), out Syncer syncer))
            {
                return syncer as T;
            }
            return null;
        }

        public void RemoveSyncer<T>() where T : Syncer
        {
            while (!Syncers.TryRemove(typeof(T), out _)) ;
        }

        public void Reset()
        {
            Initialized = false;
            if (GameObject != null) GameObject.Destroy(GameObject);
            GameObject = null;
        }

        public void NetworkEarlyUpdate()
        {
            if (!Initialized) Initialize();
            foreach (var syncer in Syncers.Values)
            {
                syncer.NetworkEarlyUpdate(this);
            }
        }

        public void NetworkLateUpdate()
        {
            if (!Initialized) Initialize();
            foreach (var syncer in Syncers.Values)
            {
                syncer.NetworkLateUpdate(this);
            }

            int origin = Syncers.Count;
            foreach (var syncer in Syncers.Values)
            {
                if (syncer.IsChanged)
                {
                    Game.Instance.Client.Send(new SyncerMessage(WorldId, EntityId, syncer)).Wait();
                }
                syncer.IsChanged = false;
            }
        }

        public void Dispose()
        {
            Reset();
        }
    }
}