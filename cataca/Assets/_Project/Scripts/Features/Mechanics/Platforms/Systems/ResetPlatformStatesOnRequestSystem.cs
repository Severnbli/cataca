using System.Linq;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class ResetPlatformStatesOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<TweenComponent> _tweenPool;
        private EcsPool<TweenQueueComponent> _tweenQueuePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<ResetPlatformStatesRequest>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _tweenPool = world.GetPool<TweenComponent>();
            _tweenQueuePool = world.GetPool<TweenQueueComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var platform = ref _platformPool.Get(e);
                ResetPlatformState(ref platform);

                if (_tweenPool.TryGetComponent(e, out var tween)) tween.Tween?.Kill();
                _tweenQueuePool.DelComponentIfExists(e);
            }
        }

        private void ResetPlatformState(ref PlatformComponent platform)
        {
            platform.PositionId = 0;
            platform.RotateId = 0;
            platform.ScaleId = 0;

            var platformTransform = platform.Platform.Object;
            var positionState = platform.Platform.PositionStates.FirstOrDefault();
            var rotateState = platform.Platform.RotateStates.FirstOrDefault();
            var scaleState = platform.Platform.ScaleStates.FirstOrDefault();

            if (positionState is not null)
            {
                platformTransform.localPosition = positionState.localPosition;
            }

            if (rotateState is not null)
            {
                platformTransform.localRotation = rotateState.localRotation;
            }

            if (scaleState is not null)
            {
                platformTransform.localScale = scaleState.localScale;
            }
        }
    }
}