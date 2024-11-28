using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
	[MemoryPackable]
	public partial class CreateWorldRespMessage : NetworkMessage
	{
		public bool Success;
		public uint WorldId;
		public CreateWorldRespMessage(bool success, uint worldId)
		{
			Success = success;
			WorldId = worldId;
			IsResponse = true;
		}

		public override void Process(Game game, int netId, KcpChannel channel)
		{
			throw new NotImplementedException();
		}
	}
}
