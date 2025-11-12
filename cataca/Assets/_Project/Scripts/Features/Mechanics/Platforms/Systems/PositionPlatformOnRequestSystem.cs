using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

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
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<PositionPlatformRequest>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _tweenPool = world.GetPool<TweenComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var platform = ref _platformPool.Get(e);

                var states = platform.Platform.States;

                var nextId = platform.PositionId + 1;
                platform.PositionId = nextId < states.Count
                    ? nextId
                    : 0;
                
                ref var tween = ref _tweenPool.Has(e)
                    ? ref _tweenPool.Get(e)
                    : ref _tweenPool.Add(e);
                
                tween.Tween ??= DOTween.Sequence();
                
                var targetPosition = states[platform.PositionId].position;
                
                tween.Tween.Append(platform.Platform.Object.transform
                    .DOMove(targetPosition, _animationConfig.Duration)
                    .SetEase(_animationConfig.Ease)
                );
                
                tween.Tween.AppendInterval(_animationConfig.TransitionDuration);
            }
        }
    }
}