using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

namespace SyncerNet.Hotfix.Messages
{
	[MemoryPackable]
	public partial class JoinGameReqMessage : NetworkMessage
	{
		public override void Process(Game game, int netId, KcpChannel channel)
		{
			game.AddPlayer(netId, PlayerId);
			game.Send(netId, new JoinGameRespMessage { Id = Id }, channel);
			Logger.Debug($"Join Game, NetworkId: {netId}, PlayerId: {PlayerId}");
		}
	}
}
