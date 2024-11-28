using kcp2k;
using MemoryPack;

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

		public override void Process(Game game, int netId, KcpChannel channel)
		{

		}
	}
}
