using kcp2k;
using MemoryPack;
using UnityEngine;

#nullable enable
namespace SyncerNet.Hotfix.Messages
{
    [MemoryPackable]
    public partial class SyncerMessage : NetworkMessage
    {
        public uint WorldId;
        public uint EntityId;

        public Syncer Syncer;

        public SyncerMessage(uint worldId, uint entityId, Syncer syncer)
        {
            WorldId = worldId;
            EntityId = entityId;
            Syncer = syncer;
        }

        public override void Process(Game game, KcpChannel channel)
        {
            if (Syncer == null) return;
            World? world = game.GetWorld(WorldId);
            if (world == null) return;
            Entity? entity = world.GetEntity(EntityId);
            if (entity == null) return;

            Syncer?.UpdateSyncer(entity);
        }
    }
}
