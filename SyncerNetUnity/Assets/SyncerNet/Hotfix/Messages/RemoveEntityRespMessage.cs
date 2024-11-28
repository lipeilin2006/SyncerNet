using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class RemoveEntityRespMessage : NetworkMessage
    {
        public bool Success;
        public RemoveEntityRespMessage(bool success)
        {
            Success = success;
            IsResponse = true;
        }

        public override void Process(Game game, KcpChannel channel)
        {

        }
    }
}