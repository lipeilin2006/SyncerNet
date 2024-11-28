using MemoryPack;
using System.Collections.Concurrent;

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

		public Entity(uint entityId, uint worldId, uint ownerId, string prefabPath, ConcurrentDictionary<Type, Syncer> syncers)
		{
			EntityId = entityId;
			WorldId = worldId;
			OwnerId = ownerId;
			PrefabPath = prefabPath;
			Syncers = syncers;
		}

		/// <summary>
		/// 添加或设置Syncer
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tSyncer"></param>
		public void AddOrSetSyncer<T>(T tSyncer) where T : Syncer
		{
			if (!Syncers.TryAdd(typeof(T), tSyncer))
			{
				Syncers[typeof(T)] = tSyncer;
			}
		}
	}
}
