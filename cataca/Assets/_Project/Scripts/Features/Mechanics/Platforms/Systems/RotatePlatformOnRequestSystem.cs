using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Requests;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class RotatePlatformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public RotatePlatformOnRequestSystem(PlatformAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }
        
        private PlatformAnimationConfig _animationConfig;
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<TweenQueueAppendRequest> _tweenQueueAppendRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<RotatePlatformRequest>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _tweenQueueAppendRequestPool = world.GetPool<TweenQueueAppendRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                if (_tweenQueueAppendRequestPool.Has(e)) continue;
                
                ref var platform = ref _platformPool.Get(e);

                var states = platform.Platform.RotateStates;
                if (!states.Any()) continue;

                var nextId = platform.RotateId + 1;
                platform.RotateId = nextId < states.Count
                    ? nextId
                    : 0;
                
                ref var tweenQueueAppendRequest = ref _tweenQueueAppendRequestPool.Add(e);
                
                var targetRotation = states[platform.RotateId].rotation;
                var localPlatform = platform.Platform;
                
                tweenQueueAppendRequest.Func = () =>
                {
                    var sequence = DOTween.Sequence();

                    sequence.Append(localPlatform.Object.transform
                        .DORotateQuaternion(targetRotation, _animationConfig.Duration)
                        .SetEase(_animationConfig.Ease)
                    );
                
                    sequence.AppendInterval(_animationConfig.TransitionDuration);
                    
                    return sequence;
                };
            }
        }
    }
}