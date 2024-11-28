using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

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

		public override void Process(Game game, int netId, KcpChannel channel)
		{
			Logger.Debug($"Join World, PlayerId: {PlayerId}, WorldId: {WorldId}");
			Player? player = game.GetPlayer(netId);
			World? world = game.GetWorld(WorldId);
			if (player != null && world != null)
			{
				game.Send(netId, new JoinWorldRespMessage(world.TryJoinPlayer(player), world) { Id = Id }, channel);
				return;
			}
			game.Send(netId, new JoinWorldRespMessage(false, null), channel);
		}
	}
}
