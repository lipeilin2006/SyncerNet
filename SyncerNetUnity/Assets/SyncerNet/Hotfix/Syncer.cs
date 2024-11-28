using MemoryPack;
using SyncerNet.Hotfix.Syncers;

#nullable enable
namespace SyncerNet.Hotfix
{
    /// <summary>
    /// 这个类的数据结构和MemoryPack特性标签需要和服务端一致(函数除外)
    /// </summary>
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(AnimatorSyncer))]
    [MemoryPackUnion(1, typeof(TransformSyncer))]
    public abstract partial class Syncer
    {
        [MemoryPackIgnore]
        public bool IsChanged = false;
        public abstract void UpdateSyncer(Entity entity);

        public virtual void NetworkEarlyUpdate(Entity entity) { }

        public virtual void NetworkLateUpdate(Entity entity) { }
    }
}
