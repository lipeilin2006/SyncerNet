using MemoryPack;
using UnityEngine;

#nullable enable
namespace SyncerNet.Hotfix.Syncers
{
    [MemoryPackable]
    public partial class TransformSyncer : Syncer
    {
        [MemoryPackInclude]
        private Vector3 _position = Vector3.zero;
        [MemoryPackInclude]
        private Vector3 _eulerAngles = Vector3.zero;
        [MemoryPackInclude]
        private Vector3 _localScale = Vector3.zero;

        [MemoryPackIgnore]
        private float _positionAccuracy = 0.01f;
        [MemoryPackIgnore]
        private float _rotationAccuracy = 0.01f;
        [MemoryPackIgnore]
        private float _scaleAccuracy = 0.01f;


        public override void UpdateSyncer(Entity entity)
        {
            entity.AddOrSetSyncer(this);
        }


        public override void NetworkEarlyUpdate(Entity entity)
        {
            if (!entity.IsLocal)
            {
                RemoteToLocal(entity);
            }
        }

        public override void NetworkLateUpdate(Entity entity)
        {
            if (entity.IsLocal)
            {
                LocalToRemote(entity);
            }
        }

        private void LocalToRemote(Entity entity)
        {
            if (entity.GameObject == null) return;

            if (
                Vector3.Distance(entity.GameObject.transform.position, _position) > _positionAccuracy ||
                Vector3.Distance(entity.GameObject.transform.eulerAngles, _eulerAngles) > _rotationAccuracy ||
                Vector3.Distance(entity.GameObject.transform.localScale, _localScale) > _scaleAccuracy
                )
            {
                _position = entity.GameObject.transform.position;
                _eulerAngles = entity.GameObject.transform.eulerAngles;
                _localScale = entity.GameObject.transform.localScale;
                IsChanged = true;
            }
        }

        private void RemoteToLocal(Entity entity)
        {
            if (entity.GameObject == null) return;

            entity.GameObject.transform.position = _position;
            entity.GameObject.transform.eulerAngles = _eulerAngles;
            entity.GameObject.transform.localScale = _localScale;
        }
    }
}
