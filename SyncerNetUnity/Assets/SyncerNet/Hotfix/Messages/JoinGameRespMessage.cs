using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class JoinGameRespMessage : NetworkMessage
    {
        public JoinGameRespMessage()
        {
            IsResponse = true;
        }
        public override void Process(Game game, KcpChannel channel)
        {
        }
    }
}
