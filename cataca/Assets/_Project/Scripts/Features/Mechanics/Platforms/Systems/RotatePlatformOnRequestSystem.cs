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
        private EcsPool<SequenceComponent> _sequencePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<RotatePlatformRequest>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _sequencePool = world.GetPool<SequenceComponent>();
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
                
                ref var sequence = ref _sequencePool.Has(e)
                    ? ref _sequencePool.Get(e)
                    : ref _sequencePool.Add(e);
                
                if (sequence.Sequence is not { active: true }) sequence.Sequence = DOTween.Sequence();
                
                var targetRotation = states[platform.RotateId].rotation;
                
                sequence.Sequence.Append(platform.Platform.Object.transform
                    .DORotateQuaternion(targetRotation, _animationConfig.Duration)
                    .SetEase(_animationConfig.Ease)
                );
                
                sequence.Sequence.AppendInterval(_animationConfig.TransitionDuration);
            }
        }
    }
}