using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PlatformGlowUpdatePulseSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public PlatformGlowUpdatePulseSystem(PlatformAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }
        
        private PlatformAnimationConfig _animationConfig;
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<PlatformGlowComponent> _platformGlowPool;
        private EcsPool<TweenComponent> _tweenPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .Inc<PlatformGlowComponent>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _platformGlowPool = world.GetPool<PlatformGlowComponent>();
            _tweenPool = world.GetPool<TweenComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var platformGlow = ref _platformGlowPool.Get(e);

                if (!_tweenPool.Has(platformGlow.PulseAnimEntity)) continue;
                
                ref var tween = ref _tweenPool.Get(platformGlow.PulseAnimEntity);
                if (tween.Tween is { active: true}) continue;
                
                ref var platform = ref _platformPool.Get(e);

                var pulseValue = platform.Platform.SpriteGlow.GlowBrightness + _animationConfig.BrightnessPulseFactor; 
                
                var sequence = DOTween.Sequence();

                sequence.Append(platform.Platform.SpriteGlow
                    .DOBrightness(pulseValue, _animationConfig.BrightnessPulseDuration)
                    .SetEase(_animationConfig.ColorEase));
                
                sequence.Append(platform.Platform.SpriteGlow
                    .DOBrightness(platform.Platform.SpriteGlow.GlowBrightness, _animationConfig.BrightnessPulseDuration)
                    .SetEase(_animationConfig.ColorEase));
                
                tween.Tween = sequence;
            }
        }
    }
}