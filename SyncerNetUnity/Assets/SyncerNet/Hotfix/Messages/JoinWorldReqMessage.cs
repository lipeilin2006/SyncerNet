using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class JoinWorldReqMessage : NetworkMessage
    {
        public uint WorldId;
        public JoinWorldReqMessage(uint worldId)
        {
            WorldId = worldId;
        }

        public override void Process(Game game, KcpChannel channel)
        {
        }
    }
}
