using MemoryPack;
using SyncerNet.Hotfix.Messages;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace SyncerNet.Hotfix
{
    [MemoryPackable]
    public partial class World
    {
        public uint WorldId { get; set; }
        public ConcurrentDictionary<uint, Entity> Entities { get; set; } = new();

        [MemoryPackIgnore]
        public bool IsActive { get; private set; } = false;

        public void NetworkEarlyUpdate()
        {
            foreach (var entity in Entities.Values)
            {
                entity.NetworkEarlyUpdate();
            }
        }

        public void NetworkLateUpdate()
        {
            foreach (var entity in Entities.Values)
            {
                entity.NetworkLateUpdate();
            }
        }

        /// <summary>
        /// 设置World的启用状态。只有启用的World会进行网络同步。
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            if (IsActive && !active)
            {
                IsActive = active;
                Reset();
            }
            else if (!IsActive && active)
            {
                IsActive = active;
            }
        }

        /// <summary>
        /// 获取Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity? GetEntity(uint id)
        {
            if (Entities.TryGetValue(id, out Entity? entity))
                return entity;
            return null;
        }

        /// <summary>
        /// 添加Entity
        /// </summary>
        /// <param name="prefabPath">Prefab路径，留空则不创建游戏对象</param>
        /// <returns>(isSuccess,EntityId)</returns>
        public async Task<(bool, uint)> TryAddEntity(string prefabPath = "")
        {
            Game game = Game.Instance;
            AddEntityRespMessage? response = await game.Client.Send(new AddEntityReqMessage(WorldId, PlayerConfigs.PlayerId, prefabPath), true) as AddEntityRespMessage;

            if (response == null) return (false, 0);
            if (!response.Success) return (false, 0);

            Entity entity = new Entity(response.EntityId, PlayerConfigs.PlayerId, prefabPath, this);
            while (!Entities.TryAdd(response.EntityId, entity)) ;
            return (response.Success, response.EntityId);
        }

        /// <summary>
        /// 移除Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>isSuccess</returns>
        public async Task<bool> TryRemoveEntity(uint entityId)
        {
            Game? game = Game.Instance;
            RemoveEntityRespMessage? response = await game.Client.Send(new RemoveEntityReqMessage(WorldId, entityId), true) as RemoveEntityRespMessage;

            if (response == null) return false;
            if (!response.Success) return false;
            Entities.Remove(entityId, out Entity Entity);
            Entity.Dispose();
            return true;
        }

        /// <summary>
        /// 重置World，移除所有已创建的Unity游戏对象（物体），不会移除Entity
        /// </summary>
        public void Reset()
        {
            foreach (var Entity in Entities.Values)
            {
                Entity.Reset();
            }
        }
    }
}
