using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

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

		public override void Process(Game game, int netId, KcpChannel channel)
		{
			Logger.Debug($"Remove Entity, EntityId: {EntityId}");
			World? world = game.GetWorld(WorldId);
			if (world != null)
			{
				game.Send(netId, new RemoveEntityRespMessage(world.TryRemoveEntity(EntityId)) { Id = Id }, channel);
			}
			game.Send(netId, new RemoveEntityRespMessage(false) { Id = Id }, channel);
		}
	}
}