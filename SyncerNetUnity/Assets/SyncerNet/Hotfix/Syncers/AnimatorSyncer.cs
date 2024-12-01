using MemoryPack;
using System;
using UnityEngine;

#nullable enable
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


        [MemoryPackIgnore]
        private int[]? _animationHash;
        [MemoryPackIgnore]
        private int[]? _transitionHash;

        [MemoryPackIgnore]
        private bool _used = false;

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

			if (entity.GameObject.TryGetComponent(out Animator animator))
			{
				int parameterCount = animator.parameterCount;
				int layerCount = animator.layerCount;

				if (_intParameters == null) { _intParameters = new int[parameterCount]; IsChanged = true; }
				if (_floatParameters == null) { _floatParameters = new float[parameterCount]; IsChanged = true; }
				if (_boolParameters == null) { _boolParameters = new bool[parameterCount]; IsChanged = true; }

				if (_animationHash == null) { _animationHash = new int[layerCount]; IsChanged = true; }
				if (_transitionHash == null) { _transitionHash = new int[layerCount]; IsChanged = true; }
				if (_stateHash == null) { _stateHash = new int[layerCount]; IsChanged = true; }
				if (_layerWeight == null) { _layerWeight = new float[layerCount]; IsChanged = true; }
				if (_normalizedTime == null) { _normalizedTime = new float[layerCount]; IsChanged = true; }

				//Speed
				_animatorSpeed = animator.speed;

				//Parameters
				for (int i = 0; i < animator.parameterCount; i++)
				{
					var parameter = animator.parameters[i];
					if (parameter.type == AnimatorControllerParameterType.Int)
					{
						_intParameters[i] = animator.GetInteger(parameter.nameHash);
					}
					else if (parameter.type == AnimatorControllerParameterType.Float)
					{
						_floatParameters[i] = animator.GetFloat(parameter.nameHash);
					}
					else if (parameter.type == AnimatorControllerParameterType.Bool)
					{
						_boolParameters[i] = animator.GetBool(parameter.nameHash);
					}
				}

				//State
				for (int i = 0; i < animator.layerCount; i++)
				{
					_layerWeight[i] = animator.GetLayerWeight(i);

					if (animator.IsInTransition(i))
					{
						AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(i);
						_transitionHash[i] = transitionInfo.fullPathHash;
						_animationHash[i] = 0;
					}

					AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(i);
					_animationHash[i] = stateInfo.fullPathHash;
					_transitionHash[i] = 0;
					_stateHash[i] = stateInfo.fullPathHash;
					_normalizedTime[i] = stateInfo.normalizedTime;

					IsChanged = true;
				}
			}
        }

        private void RemoteToLocal(Entity entity)
        {
            if (_used) return;
            if (entity.GameObject == null) return;

            if (entity.GameObject.TryGetComponent(out Animator animator))
            {
                //Speed
                animator.speed = _animatorSpeed;

                //Parameters
                if (_intParameters == null) return;
                if (_floatParameters == null) return;
                if (_boolParameters == null) return;

                //State
                if (_animationHash == null) return;
                if (_stateHash == null) return;
                if (_layerWeight == null) return;
                if (_normalizedTime == null) return;


                //Update Parameters
                for (int i = 0; i < animator.parameterCount; i++)
                {
                    var parameter = animator.parameters[i];
                    if (parameter.type == AnimatorControllerParameterType.Int)
                    {
                        animator.SetInteger(parameter.nameHash, _intParameters[i]);
                    }
                    else if (parameter.type == AnimatorControllerParameterType.Float)
                    {
                        animator.SetFloat(parameter.nameHash, _floatParameters[i]);
                    }
                    else if (parameter.type == AnimatorControllerParameterType.Bool)
                    {
                        animator.SetBool(parameter.nameHash, _boolParameters[i]);
                    }
                }

                //Update State
                for (int i = 0; i < animator.layerCount; i++)
                {
                    animator.SetLayerWeight(i, _layerWeight[i]);
                    animator.Play(_stateHash[i], i, _normalizedTime[i]);
                }
            }
            _used = true;
        }
    }
}
