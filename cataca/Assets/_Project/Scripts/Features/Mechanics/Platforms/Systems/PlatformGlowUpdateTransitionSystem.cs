using System.Linq;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Configs;
using _Project.Scripts.Features.Mechanics.Platforms.Enums;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PlatformGlowUpdateTransitionSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public PlatformGlowUpdateTransitionSystem(PlatformAnimationConfig animationConfig)
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

                if (!_tweenPool.Has(platformGlow.TransitionAnimEntity)) continue;
                
                ref var tween = ref _tweenPool.Get(platformGlow.TransitionAnimEntity);
                if (tween.Tween is { active: true}) continue;
                
                ref var platform = ref _platformPool.Get(e);
                
                var finalColor = GetFinalColor(ref platform, ref platformGlow);

                var sequence = DOTween.Sequence();

                sequence.Append(platform.Platform.SpriteGlow
                    .DOColor(finalColor, _animationConfig.ColorTransitionDuration)
                    .SetEase(_animationConfig.ColorEase));
                
                sequence.AppendInterval(_animationConfig.ColorTransitionPauseDuration);

                tween.Tween = sequence;
            }
        }

        private Color GetFinalColor(ref PlatformComponent platform, ref PlatformGlowComponent platformGlow)
        {
            if (TryGetColor(ref platform, ref platformGlow, out var finalColor)) return finalColor;
            
            platformGlow.PositionWas = false;
            platformGlow.RotateWas = false;
            platformGlow.ScaleWas = false;
                
            TryGetColor(ref platform, ref platformGlow, out finalColor);

            return finalColor;
        }

        private bool TryGetColor(ref PlatformComponent platform, ref PlatformGlowComponent platformGlow, 
            out Color color)
        {
            color = _animationConfig.BaseColor;
            
            if (platform.Platform.PlatformTypes.Contains(PlatformType.Position) && !platformGlow.PositionWas)
            {
                platformGlow.PositionWas = true;
                color = _animationConfig.PositionColor;
                return true;
            }

            if (platform.Platform.PlatformTypes.Contains(PlatformType.Rotate) && !platformGlow.RotateWas)
            {
                platformGlow.RotateWas = true;
                color = _animationConfig.RotateColor;
                return true;
            }

            if (platform.Platform.PlatformTypes.Contains(PlatformType.Scale) && !platformGlow.ScaleWas)
            {
                platformGlow.ScaleWas = true;
                color = _animationConfig.ScaleColor;
                return true;
            }

            return false;
        }
    }
}