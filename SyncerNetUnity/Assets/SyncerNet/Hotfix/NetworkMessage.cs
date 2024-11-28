using kcp2k;
using MemoryPack;
using SyncerNet.Hotfix.Messages;

namespace SyncerNet.Hotfix
{
    /// <summary>
    /// 这个类的数据结构和MemoryPack特性标签需要和服务端一致(函数除外)
    /// </summary>
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(AddEntityMessage))]
    [MemoryPackUnion(1, typeof(AddEntityReqMessage))]
    [MemoryPackUnion(2, typeof(AddEntityRespMessage))]
    [MemoryPackUnion(3, typeof(CreateWorldReqMessage))]
    [MemoryPackUnion(4, typeof(CreateWorldRespMessage))]
    [MemoryPackUnion(5, typeof(JoinGameReqMessage))]
    [MemoryPackUnion(6, typeof(JoinGameRespMessage))]
    [MemoryPackUnion(7, typeof(JoinWorldReqMessage))]
    [MemoryPackUnion(8, typeof(JoinWorldRespMessage))]
    [MemoryPackUnion(9, typeof(RemoveEntityReqMessage))]
    [MemoryPackUnion(10, typeof(RemoveEntityRespMessage))]
    [MemoryPackUnion(11, typeof(SyncerMessage))]
    public abstract partial class NetworkMessage
    {
        public uint Id;
        public uint PlayerId;
        public string Token;
        public bool IsResponse = false;
        public abstract void Process(Game game, KcpChannel channel);
    }
}