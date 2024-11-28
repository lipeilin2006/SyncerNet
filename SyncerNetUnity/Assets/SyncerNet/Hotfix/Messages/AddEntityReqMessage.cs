using kcp2k;
using MemoryPack;

#nullable enable
namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class AddEntityReqMessage : NetworkMessage
    {
        public uint WorldId;
        public uint OwnerId;
        public string PrefabPath;

        public AddEntityReqMessage(uint worldId, uint ownerId, string prefabPath)
        {
            WorldId = worldId;
            OwnerId = ownerId;
            PrefabPath = prefabPath;
        }

        public override void Process(Game game, KcpChannel channel)
        {

        }
    }
}