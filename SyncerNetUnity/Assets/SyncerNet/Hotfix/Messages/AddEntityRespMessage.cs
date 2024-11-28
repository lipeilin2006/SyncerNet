using kcp2k;
using MemoryPack;
using System;

namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class AddEntityRespMessage : NetworkMessage
    {
        public bool Success;
        public uint EntityId;
        public AddEntityRespMessage(bool success, uint entityId)
        {
            Success = success;
            EntityId = entityId;
            IsResponse = true;
        }

        public override void Process(Game game, KcpChannel channel)
        {
            throw new NotImplementedException();
        }
    }
}
