using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class RemoveEntityReqMessage : NetworkMessage
    {
        public uint WorldId;
        public uint EntityId;
        public RemoveEntityReqMessage(uint worldId, uint entityId)
        {
            WorldId = worldId;
            EntityId = entityId;
        }

        public override void Process(Game game, KcpChannel channel)
        {

        }
    }
}