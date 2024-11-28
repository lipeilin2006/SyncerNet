using MemoryPack;
using System.Collections.Generic;
using SyncerNet.Hotfix.Syncers;

namespace SyncerNet.Hotfix
{
    [MemoryPackable]
    [MemoryPackUnion(0,typeof(AnimatorSyncer))]
    [MemoryPackUnion(1,typeof(TransformSyncer))]
    public abstract partial class Syncer
    {
        /// <summary>
        /// 接收到SyncerMessage后，对Syncer进行更新
        /// </summary>
        /// <param name="entity"></param>
        public abstract void UpdateSyncer(Entity entity);
    }
}