using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

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

		public override void Process(Game game, int netId, KcpChannel channel)
		{
			Logger.Debug(Syncer.GetType().Name);
			if (Syncer == null) return;
			World? world = game.GetWorld(WorldId);
			if (world == null) return;
			Entity? entity = world.GetEntity(EntityId);
			if (entity == null) return;

			//更新Syncer
			Syncer.UpdateSyncer(entity);
			foreach (Player player in world.Players.Values)
			{
				if (player.PlayerId != PlayerId) game.Send(player.NetworkId, this, channel);
			}
		}
	}
}
