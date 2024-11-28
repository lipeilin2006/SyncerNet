using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class CreateWorldReqMessage : NetworkMessage
    {
        public override void Process(Game game, KcpChannel channel)
        {
        }
    }
}