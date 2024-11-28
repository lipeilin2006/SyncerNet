using kcp2k;
using MemoryPack;
using SyncerNet.Logging;
using System.Collections.Concurrent;

namespace SyncerNet.Hotfix
{
	/// <summary>
	/// Game为全局唯一的根节点，所有World的父节点
	/// </summary>
	public class Game
	{
		/// <summary>
		/// Key:WorldId,Value:World
		/// </summary>
		public ConcurrentDictionary<uint, World> Worlds { get; set; } = new();
		/// <summary>
		/// Key:NetworkId,Value:Player
		/// </summary>
		public ConcurrentDictionary<int, Player> Players { get; set; } = new();

		/// <summary>
		/// 用于生成唯一WorldId
		/// </summary>
		private readonly Random _random = new Random();

		/// <summary>
		/// Hotfix时赋值的NetworkServer的Send函数
		/// </summary>
		public Action<int, ArraySegment<byte>, KcpChannel>? SendAction { get; set; }

		public Game()
		{
			Logger.Info("Initializd");
		}

		public void OnConnected(int netId)
		{

		}
		public void ProcessMessage(int netId, ArraySegment<byte> data, KcpChannel channel)
		{
			try
			{
				Logger.Debug(data.Count);
				NetworkMessage? message = MemoryPackSerializer.Deserialize<NetworkMessage>(data);
				if (message != null)
				{
					if (Authenticator.Authenticate(message))
					{
						message.Process(this, netId, channel);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Debug($"{string.Join(',', data)}");
				Logger.Error(ex.ToString());
			}
		}
		public void OnDisconnected(int netId)
		{
			Player? player = GetPlayer(netId);
			if (player == null) return;
			GetWorld(player.WorldId)?.RemovePlayer(player.PlayerId);
		}

		public void OnError(int netId, ErrorCode errorCode, string message)
		{
			Logger.Error(message);
		}

		/// <summary>
		/// 向指定NetworkId的玩家发送Byte[]消息
		/// </summary>
		/// <param name="netId">NetworkId</param>
		/// <param name="data"></param>
		/// <param name="channel"></param>
		public void Send(int netId, ArraySegment<byte> data, KcpChannel channel)
		{
			SendAction?.Invoke(netId, data, channel);
		}

		/// <summary>
		/// 向指定NetworkId的玩家发送Message消息
		/// </summary>
		/// <param name="netId"></param>
		/// <param name="message"></param>
		/// <param name="channel"></param>
		public void Send(int netId, NetworkMessage message, KcpChannel channel)
		{
			SendAction?.Invoke(netId, MemoryPackSerializer.Serialize(message), channel);
		}
		/// <summary>
		/// 生成唯一WorldId
		/// </summary>
		/// <returns>WorldId</returns>
		public uint GenerateWorldId()
		{
			while (true)
			{
				uint id = (uint)_random.Next();
				//0为保留WorldId
				if (!Worlds.ContainsKey(id) && id != 0) return id;
			}
		}

		/// <summary>
		/// 创建World
		/// </summary>
		/// <returns>(isSuccess,worldId)</returns>
		public (bool, uint) TryCreateWorld()
		{
			uint worldId = GenerateWorldId();
			return (Worlds.TryAdd(worldId, new World { WorldId = worldId }), worldId);
		}

		/// <summary>
		/// 获取World
		/// </summary>
		/// <param name="worldId"></param>
		/// <returns></returns>
		public World? GetWorld(uint worldId)
		{
			return Worlds.GetValueOrDefault(worldId);
		}

		/// <summary>
		/// 添加Player到Game
		/// </summary>
		/// <param name="netId">NetworkId</param>
		/// <param name="playerId"></param>
		public void AddPlayer(int netId, uint playerId)
		{
			Players.TryAdd(netId, new Player(netId, playerId));
		}

		/// <summary>
		/// 获取Player
		/// </summary>
		/// <param name="netId">NetworkId</param>
		/// <returns></returns>
		public Player? GetPlayer(int netId)
		{
			return Players.GetValueOrDefault(netId);
		}
	}
}
