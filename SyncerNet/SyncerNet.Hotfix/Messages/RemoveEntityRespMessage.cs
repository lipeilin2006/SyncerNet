using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
	[MemoryPackable]
	public partial class RemoveEntityRespMessage : NetworkMessage
	{
		public bool Success;
		public RemoveEntityRespMessage(bool success)
		{
			IsResponse = true;
			Success = success;
		}

		public override void Process(Game game, int netId, KcpChannel channel)
		{

		}
	}
}