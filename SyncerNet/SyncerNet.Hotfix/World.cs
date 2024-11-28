using MemoryPack;
using System.Collections.Concurrent;

namespace SyncerNet.Hotfix
{
	/// <summary>
	/// 可序列化的World，新玩家加入时，无需重复发送之前的消息，只需要将此序列化后发送给玩家，便可同步最新的World和其中的Entity
	/// </summary>
	[MemoryPackable]
	public partial class World
	{
		public uint WorldId { get; set; }
		/// <summary>
		/// Key:EntityId,Value:Entity
		/// </summary>
		public ConcurrentDictionary<uint, Entity> Entities { get; set; } = new();

		/// <summary>
		/// 用于生成唯一EntityId
		/// </summary>
		[MemoryPackIgnore]
		private readonly Random _random = new Random();
		/// <summary>
		/// 该World中的所有玩家
		/// </summary>
		[MemoryPackIgnore]
		public ConcurrentDictionary<uint, Player> Players { get; set; } = new();

		/// <summary>
		/// 获取Entity
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		public Entity? GetEntity(uint entityId)
		{
			return Entities.GetValueOrDefault(entityId);
		}

		/// <summary>
		/// 将Player加入到该World
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public bool TryJoinPlayer(Player player)
		{
			Players.TryAdd(player.PlayerId, player);
			player.WorldId = WorldId;
			return true;
		}

		/// <summary>
		/// 将Player从该World中移除
		/// </summary>
		/// <param name="playerId"></param>
		public void RemovePlayer(uint playerId)
		{
			foreach (Entity entity in Entities.Values)
			{
				if (entity.OwnerId == playerId)
				{
					while (!TryRemoveEntity(entity.EntityId)) ;
				}
			}
			while (Players.TryRemove(playerId, out _)) ;
		}

		/// <summary>
		/// 生成唯一EntityId
		/// </summary>
		/// <returns>EntityId</returns>
		public uint GenerateEntityId()
		{
			while (true)
			{
				uint entityId = (uint)_random.Next();
				//0为保留PlayerId，用于没有任何所有者的Entity
				if (!Entities.ContainsKey(entityId) && entityId != 0) return entityId;
			}
		}

		/// <summary>
		/// 添加Entity
		/// </summary>
		/// <param name="ownerId">所有者的PlayerId</param>
		/// <param name="prfabPath"></param>
		/// <returns>(isSuccess,EntityId)</returns>
		public (bool, uint) TryAddEntity(uint ownerId, string prfabPath)
		{
			uint entityId = GenerateEntityId();
			Entity entity = new Entity(entityId, WorldId, ownerId, prfabPath, new());
			if (Entities.TryAdd(entityId, entity))
			{
				return (true, entityId);
			}
			return (false, 0);
		}

		/// <summary>
		/// 设置Entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>isSuccess</returns>
		public bool TrySetEntity(Entity entity)
		{
			if (Entities.ContainsKey(entity.EntityId))
			{
				Entities[entity.EntityId] = entity;
				return true;
			}
			return false;
		}

		/// <summary>
		/// 移除Entity
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns>isSuccess</returns>
		public bool TryRemoveEntity(uint entityId)
		{
			if (Entities.TryRemove(entityId, out _))
			{
				return true;
			}
			return false;
		}
	}
}
