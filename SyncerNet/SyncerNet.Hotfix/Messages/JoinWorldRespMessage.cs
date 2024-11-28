using kcp2k;
using MemoryPack;

namespace SyncerNet.Hotfix.Messages
{
	[MemoryPackable]
	public partial class JoinWorldRespMessage : NetworkMessage
	{
		public bool Success;
		public World? World;
		public JoinWorldRespMessage(bool success, World? world)
		{
			IsResponse = true;
			Success = success;
			World = world;
		}

		public override void Process(Game game, int netId, KcpChannel channel)
		{

		}
	}
}
