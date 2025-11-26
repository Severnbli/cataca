using System.Linq;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Requests;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PositionPlatformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PositionPlatformOnRequestSystem(PlatformAnimationConfig animationConfig)
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
                .Inc<PositionPlatformRequest>()
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

                var states = platform.Platform.PositionStates;
                if (!states.Any()) continue;

                var nextId = platform.PositionId + 1;
                platform.PositionId = nextId < states.Count
                    ? nextId
                    : 0;
                
                ref var tweenQueueAppendRequest = ref _tweenQueueAppendRequestPool.Add(e);
                
                var targetPosition = states[platform.PositionId].position;
                var localPlatform = platform.Platform;
                
                tweenQueueAppendRequest.Func = () =>
                {
                    var sequence = DOTween.Sequence();

                    var currentTransform = localPlatform.Object.transform;
                    var time = DOTweenUtils.GetTimeToChange(currentTransform.position, targetPosition,
                        _animationConfig.PositionTransitionSpeed);
                    
                    sequence.Append(currentTransform
                        .DOMove(targetPosition, time)
                        .SetEase(_animationConfig.TransformEase)
                    );

                    sequence.AppendInterval(_animationConfig.TransformTransitionDuration);

                    return sequence;
                };
            }
        }
    }
}