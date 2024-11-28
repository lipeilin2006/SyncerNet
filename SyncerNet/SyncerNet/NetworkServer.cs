using kcp2k;
using System.Collections.Concurrent;

namespace SyncerNet
{
	/// <summary>
	/// 网络服务器
	/// </summary>
	public class NetworkServer
	{
		/// <summary>
		/// 用于热更，处理消息时调用
		/// </summary>
		public Action<int, ArraySegment<byte>, KcpChannel>? ProcessMessage { get; set; }
		/// <summary>
		/// 用于热更，建立新连接时调用
		/// </summary>
		public Action<int>? OnConnected { get; set; }
		/// <summary>
		/// 用于热更，断开连接时调用
		/// </summary>
		public Action<int>? OnDisconnected { get; set; }
		/// <summary>
		/// 用于热更，错误时调用
		/// </summary>

		public Action<int, ErrorCode, string>? OnError { get; set; }

		//Kcp参数
		public bool DualMode = true;
		public bool NoDelay = true;
		public uint Interval = 1;
		public int Timeout = 10000;
		public int RecvBufferSize = 1024 * 1024 * 4;
		public int SendBufferSize = 1024 * 1024 * 4;
		public int FastResend = 2;
		public uint ReceiveWindowSize = 1024 * 4;
		public uint SendWindowSize = 1024 * 4;
		public uint MaxRetransmits = Kcp.DEADLINK * 2;

		private KcpServer? _server;
		private bool _isStop = true;
		/// <summary>
		/// 消息队列
		/// </summary>
		private ConcurrentQueue<(int, ArraySegment<byte>, KcpChannel)> MessageQueue { get; set; } = new();

		/// <summary>
		/// 初始化服务器
		/// </summary>
		/// <param name="port">端口</param>
		public void Init(ushort port)
		{
			_isStop = false;
			_server = new(
				Connected,
				Data,
				Disconnected,
				Error,
				new KcpConfig()
				{
					DualMode = DualMode,
					NoDelay = NoDelay,
					Interval = Interval,
					Timeout = Timeout,
					RecvBufferSize = RecvBufferSize,
					SendBufferSize = SendBufferSize,
					FastResend = FastResend,
					ReceiveWindowSize = ReceiveWindowSize,
					SendWindowSize = SendWindowSize,
					MaxRetransmits = MaxRetransmits
				});
			_server.Start(port);
		}

		public void Send(int netId, ArraySegment<byte> data, KcpChannel channel)
		{
			_server?.Send(netId, data, channel);
		}

		private void Connected(int netId)
		{
			OnConnected?.Invoke(netId);
		}

		private void Data(int netId, ArraySegment<byte> data, KcpChannel channel)
		{
			MessageQueue.Enqueue((netId, data.ToArray(), channel));
		}

		private void Disconnected(int netId)
		{
			OnDisconnected?.Invoke(netId);
		}

		private void Error(int netId, ErrorCode errorCode, string message)
		{
			OnError?.Invoke(netId, errorCode, message);
		}

		/// <summary>
		/// 服务器单次Tick，接收消息并处理
		/// </summary>
		public void ServerTick()
		{
			if (!_isStop)
			{
				if (ProcessMessage != null)
				{
					_server?.TickIncoming();
					while (MessageQueue.Count > 0)
					{
						if (MessageQueue.TryDequeue(out var result))
						{
							ProcessMessage(result.Item1, result.Item2, result.Item3);
						}
					}
					_server?.TickOutgoing();
				}
			}
		}

		/// <summary>
		/// 服务器循环Tick接收消息并处理，会阻塞当前线程
		/// </summary>
		public void ServerLoop()
		{
			while (!_isStop)
			{
				if (ProcessMessage != null)
				{
					_server?.TickIncoming();
					while (MessageQueue.Count > 0)
					{
						if (MessageQueue.TryDequeue(out var result))
						{
							ProcessMessage(result.Item1, result.Item2, result.Item3);
						}
					}
					_server?.TickOutgoing();
				}
			}
		}
	}
}
