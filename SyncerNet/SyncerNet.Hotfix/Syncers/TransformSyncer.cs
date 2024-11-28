using MemoryPack;
using UnityEngine;

namespace SyncerNet.Hotfix.Syncers
{
	[MemoryPackable]
	public partial class TransformSyncer : Syncer
	{
		[MemoryPackInclude]
		private Vector3 _position;
		[MemoryPackInclude]
		private Vector3 _eulerAngles;
		[MemoryPackInclude]
		private Vector3 _localScale;

		public override void UpdateSyncer(Entity entity)
		{
			entity.AddOrSetSyncer(this);
		}
	}
}
