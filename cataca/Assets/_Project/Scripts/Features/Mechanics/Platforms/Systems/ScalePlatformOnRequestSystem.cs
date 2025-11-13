using System;
using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class ScalePlatformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public ScalePlatformOnRequestSystem(PlatformAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }
        
        private PlatformAnimationConfig _animationConfig;
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<TweenQueueComponent> _tweenQueuePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<ScalePlatformRequest>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _tweenQueuePool = world.GetPool<TweenQueueComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var platform = ref _platformPool.Get(e);

                var states = platform.Platform.States;

                var nextId = platform.ScaleId + 1;
                platform.ScaleId = nextId < states.Count
                    ? nextId
                    : 0;
                
                ref var tweenQueue = ref _tweenQueuePool.Has(e)
                    ? ref _tweenQueuePool.Get(e)
                    : ref _tweenQueuePool.Add(e);
                
                var targetScale = states[platform.ScaleId].localScale;
                var localPlatform = platform.Platform;
                
                tweenQueue.Queue ??= new Queue<Func<Tween>>();
                tweenQueue.Queue.Enqueue(() =>
                {
                    var sequence = DOTween.Sequence();
                    
                    sequence.Append(localPlatform.Object.transform
                        .DOScale(targetScale, _animationConfig.Duration)
                        .SetEase(_animationConfig.Ease)
                    );
                
                    sequence.AppendInterval(_animationConfig.TransitionDuration);
                    
                    return sequence;
                });
            }
        }
    }
}