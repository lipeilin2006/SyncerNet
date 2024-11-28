using kcp2k;
using MemoryPack;
using SyncerNet.Logging;

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

		public override void Process(Game game, int netId, KcpChannel channel)
		{
			World? world = game.GetWorld(WorldId);
			AddEntityRespMessage addEntityRespMessage = new(false, 0);
			addEntityRespMessage.Id = Id;

			if (world == null)
			{
				game.Send(netId, addEntityRespMessage, channel);
				return;
			}
			var result = world.TryAddEntity(OwnerId, PrefabPath);
			addEntityRespMessage.Success = result.Item1;
			addEntityRespMessage.EntityId = result.Item2;
			Logger.Debug($"Add Entity, WorldId: {WorldId}, OwnerId: {OwnerId}, Result: {{{result.Item1},{result.Item2}}}");

			game.Send(netId, addEntityRespMessage, channel);
			AddEntityMessage addEntityMessage = new(WorldId, result.Item2, OwnerId, PrefabPath);
			foreach (Player player in world.Players.Values)
			{
				if (player.PlayerId != PlayerId)
				{
					game.Send(player.NetworkId, addEntityMessage, channel);
				}
			}

		}
	}
}