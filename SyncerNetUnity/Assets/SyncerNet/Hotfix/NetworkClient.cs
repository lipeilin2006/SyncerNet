using kcp2k;
using MemoryPack;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;

#nullable enable
namespace SyncerNet.Hotfix
{
    /// <summary>
    /// 网络客户端
    /// </summary>
    public class NetworkClient : MonoBehaviour
    {
        public static NetworkClient? Instance;

        public string address = "127.0.0.1";
        public ushort port = 12345;

        [Tooltip("NoDelay is recommended to reduce latency. This also scales better without buffers getting full.")]
        public bool NoDelay = true;
        [Tooltip("KCP internal update interval. 100ms is KCP default, but a lower interval is recommended to minimize latency and to scale to more networked entities.")]
        public uint Interval = 10;
        [Tooltip("KCP timeout in milliseconds. Note that KCP sends a ping automatically.")]
        public int Timeout = 10000;
        [Tooltip("Socket receive buffer size. Large buffer helps support more connections. Increase operating system socket buffer size limits if needed.")]
        public int RecvBufferSize = 1024 * 1024 * 4;
        [Tooltip("Socket send buffer size. Large buffer helps support more connections. Increase operating system socket buffer size limits if needed.")]
        public int SendBufferSize = 1024 * 1024 * 4;

        [Tooltip("KCP fastresend parameter. Faster resend for the cost of higher bandwidth. 0 in normal mode, 2 in turbo mode.")]
        public int FastResend = 2;
        [Tooltip("KCP window size can be modified to support higher loads. This also increases max message size.")]
        public uint ReceiveWindowSize = 1024 * 4;
        [Tooltip("KCP window size can be modified to support higher loads.")]
        public uint SendWindowSize = 1024 * 4;
        [Tooltip("KCP will try to retransmit lost messages up to MaxRetransmit (aka dead_link) before disconnecting.")]
        public uint MaxRetransmits = Kcp.DEADLINK * 2;

        public KcpClient? Client => _client;

        private KcpClient? _client;

        private ConcurrentDictionary<uint, NetworkMessage?> _responses = new();

        private readonly System.Random _random = new System.Random();

        private Game? _game;
        // Start is called before the first frame update
        private void Awake()
        {
#if !UNITY_EDITOR
			NetworkLoop.RuntimeInitializeOnLoad();
#endif
            _game = Game.Instance;
            DontDestroyOnLoad(this);
            _client = new(
                OnConnected,
                OnData,
                OnDisconnected,
                OnError,
                new KcpConfig()
                {
                    NoDelay = NoDelay,
                    Interval = Interval,
                    Timeout = Timeout,
                    RecvBufferSize = RecvBufferSize,
                    SendBufferSize = SendBufferSize,
                    FastResend = FastResend,
                    ReceiveWindowSize = ReceiveWindowSize,
                    SendWindowSize = SendWindowSize,
                    MaxRetransmits = MaxRetransmits,
                }
                );
            _client.Connect(address, port);
            Instance = this;
            Debug.Log("Init");
        }

        /// <summary>
        /// 发送Message消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="needToResponse">是否需要响应</param>
        /// <param name="timeout">响应超时时间</param>
        /// <returns>如果不需要响应，则返回null。如果需要响应，则在收到响应时返回响应消息，否则返回null。</returns>
        public async Task<NetworkMessage?> Send(NetworkMessage message, bool needToResponse = false, int timeout = 10000)
        {
            try
            {
                message.PlayerId = PlayerConfigs.PlayerId;
                message.Token = PlayerConfigs.Token;
                if (needToResponse)
                {
                    uint messageId = GenerateMessageId();
                    message.Id = messageId;
                    while (!_responses.TryAdd(messageId, null)) { }
                    _client?.Send(MemoryPackSerializer.Serialize(message), KcpChannel.Reliable);

                    int t = 0;
                    while (t < timeout)
                    {
                        if (_responses[messageId] != null)
                        {
                            NetworkMessage? response;
                            while (!_responses.TryRemove(messageId, out response)) { }
                            return response;
                        }
                        await Task.Delay(100);
                        t += 100;
                    }

                    while (!_responses.TryRemove(messageId, out _)) { }
                }
                else
                {
                    message.Id = 0;
                    _client?.Send(MemoryPackSerializer.Serialize(message), KcpChannel.Reliable);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
                return null;
            }
        }

        public void Disconnect()
        {
            _client?.Disconnect();
        }

        public void Reconnect()
        {
            _client?.Disconnect();
            _client?.Connect(address, port);
        }

        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        /// <summary>
        /// 生成唯一消息Id
        /// </summary>
        /// <returns></returns>
        private uint GenerateMessageId()
        {
            while (true)
            {
                uint id = (uint)_random.Next();
                //0为保留消息Id，用于不需要响应的消息
                if (!_responses.ContainsKey(id) && id != 0)
                {
                    return id;
                }
            }
        }
        private void OnConnected()
        {
            Debug.Log("Connected");
        }
        private void OnData(ArraySegment<byte> data, KcpChannel channel)
        {
            try
            {
                NetworkMessage? message = MemoryPackSerializer.Deserialize<NetworkMessage>(data);
                if (message != null)
                {
                    if (message.IsResponse)
                    {
                        _responses[message.Id] = message;
                    }
                    else
                    {
                        message.Process(_game, channel);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }

        private void OnDisconnected()
        {
            Debug.Log("Disconnected");
        }

        private void OnError(kcp2k.ErrorCode errorCode, string message)
        {
            Debug.Log("Error");
        }

        /// <summary>
        /// 在Unity的Update之前调用
        /// </summary>
        public void NetworkEarlyUpdate()
        {
            _client?.TickIncoming();
            _game?.NetworkEarlyUpdate();
        }

        /// <summary>
        /// 在Unity的LateUpdate之后调用
        /// </summary>
        public void NetworkLateUpdate()
        {
            _game?.NetworkLateUpdate();
            _client?.TickOutgoing();
        }

        private void OnDestroy()
        {
            _client?.Disconnect();
        }
    }

    public struct DataPack
    {
        public int NetId;
        public ArraySegment<byte> Data;
        public KcpChannel Channel;
        public DataPack(int netId, ArraySegment<byte> data, KcpChannel channel)
        {
            NetId = netId;
            Data = data;
            Channel = channel;
        }
    }
}
