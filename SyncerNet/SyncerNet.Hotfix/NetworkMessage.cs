using MemoryPack;
using SyncerNet.Hotfix.Messages;
using kcp2k;

namespace SyncerNet.Hotfix
{
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
        public string Token = "";
        public bool IsResponse = false;

        public abstract void Process(Game game, int netId, KcpChannel channel);
    }
}
