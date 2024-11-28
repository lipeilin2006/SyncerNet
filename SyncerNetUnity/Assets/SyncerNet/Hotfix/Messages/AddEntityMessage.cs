using kcp2k;
using MemoryPack;

#nullable enable
namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class AddEntityMessage : NetworkMessage
    {
        public uint WorldId;
        public uint EntityId;
        public uint OwnerId;
        public string PrefabPath;

        public AddEntityMessage(uint worldId, uint entityId, uint ownerId, string prefabPath)
        {
            WorldId = worldId;
            EntityId = entityId;
            OwnerId = ownerId;
            PrefabPath = prefabPath;
        }

        public override void Process(Game game, KcpChannel channel)
        {
            World? world = game.GetWorld(WorldId);
            if (world == null) return;
            world.Entities.TryAdd(EntityId, new Entity(EntityId, OwnerId, PrefabPath, world));
        }
    }
}
