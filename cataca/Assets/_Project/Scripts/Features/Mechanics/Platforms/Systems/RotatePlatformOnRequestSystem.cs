using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
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
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<RotatePlatformRequest>()
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

                var nextId = platform.RotateId + 1;
                platform.RotateId = nextId < states.Count
                    ? nextId
                    : 0;
                
                ref var tween = ref _tweenPool.Has(e)
                    ? ref _tweenPool.Get(e)
                    : ref _tweenPool.Add(e);
                
                var sequence = DOTween.Sequence();

                if (tween.Tween is not null && tween.Tween.active) sequence.Append(tween.Tween);
                
                var targetRotation = states[platform.RotateId].rotation;
                
                sequence.Append(platform.Platform.Object.transform
                    .DORotateQuaternion(targetRotation, _animationConfig.Duration)
                    .SetEase(_animationConfig.Ease)
                );
                
                sequence.AppendInterval(_animationConfig.TransitionDuration);
                
                tween.Tween = sequence;
            }
        }
    }
}