using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

namespace SyncerNet.Hotfix.Messages
{
	[MemoryPackable]
	public partial class CreateWorldReqMessage : NetworkMessage
	{
		public override void Process(Game game, int netId, KcpChannel channel)
		{
			Player? player = game.GetPlayer(netId);
			if (player == null) return;
			var result = game.TryCreateWorld();
			if (result.Item1)
			{
				game.GetWorld(result.Item2)?.TryJoinPlayer(player);
			}
			game.Send(netId, new CreateWorldRespMessage(result.Item1, result.Item2) { Id = Id }, channel);
			Logger.Debug($"Create World, Result:{{{result.Item1},{result.Item2}}}");
		}
	}
}