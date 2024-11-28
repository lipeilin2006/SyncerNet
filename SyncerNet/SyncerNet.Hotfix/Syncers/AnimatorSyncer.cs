using MemoryPack;

namespace SyncerNet.Hotfix.Syncers
{
	[MemoryPackable]
	public partial class AnimatorSyncer : Syncer
	{
		[MemoryPackInclude]
		private float _animatorSpeed = 1f;

		[MemoryPackInclude]
		private int[]? _intParameters;
		[MemoryPackInclude]
		private float[]? _floatParameters;
		[MemoryPackInclude]
		private bool[]? _boolParameters;

		// multiple layers
		[MemoryPackInclude]
		private int[]? _stateHash;
		[MemoryPackInclude]
		private float[]? _normalizedTime;
		[MemoryPackInclude]
		private float[]? _layerWeight;

		public override void UpdateSyncer(Entity entity)
		{
			entity.AddOrSetSyncer(this);
		}
	}
}
