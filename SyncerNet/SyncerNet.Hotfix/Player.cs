namespace SyncerNet.Hotfix
{
	public class Player
	{
		public int NetworkId;
		public uint PlayerId;
		public uint WorldId = 0;
		public long LastHeartBeat = 0;
		public Player(int networkId, uint playerId)
		{
			NetworkId = networkId;
			PlayerId = playerId;
		}
	}
}
