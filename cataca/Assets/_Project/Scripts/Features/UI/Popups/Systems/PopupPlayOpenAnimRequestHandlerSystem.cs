using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.UI.Popups.Components;
using _Project.Scripts.Features.UI.Popups.Configs;
using _Project.Scripts.Features.UI.Popups.Requests;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.UI.Popups.Systems
{
    public class PopupPlayOpenAnimRequestHandlerSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PopupPlayOpenAnimRequestHandlerSystem(PopupsAnimationConfig config)
        {
            _config = config;
        }
        
        private PopupsAnimationConfig _config;
        private EcsFilter _filter;
        private EcsPool<PopupPlayOpenAnimRequest> _popupPlayOpenAnimRequestPool;
        private EcsPool<PopupComponent> _popupPool;
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PopupPlayOpenAnimRequest>()
                .Inc<PopupComponent>()
                .End();
            
            _popupPlayOpenAnimRequestPool = world.GetPool<PopupPlayOpenAnimRequest>();
            _popupPool = world.GetPool<PopupComponent>();
            _tweenPool = world.GetPool<TweenComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                _popupPlayOpenAnimRequestPool.Del(e);
                
                ref var popup = ref _popupPool.Get(e);
                if (popup.Opened) continue;
                
                popup.Opened = true;
                
                ref var tween = ref _tweenPool.Has(e)
                    ? ref _tweenPool.Get(e)
                    : ref _tweenPool.Add(e);
                
                tween.Tween?.Kill();
                tween.Tween = MakeAnim(popup);
            }
        }
        
        private Tween MakeAnim(PopupComponent popup)
        {
            popup.Body.transform.localScale *= _config.OpenBodyScale;

            var c = popup.Fade.color;
            c.a = 0;
            popup.Fade.color = c;
            
            popup.Parent.SetActive(true);
            
            var sequency = DOTween.Sequence();
            
            sequency.Append(popup.Fade
                .DOFade(
                    _config.OpenFadeResultFactor / 255f, 
                    _config.OpenDuration * _config.OpenFadeDurationPercentage)
            );
            
            sequency.Join(popup.Body.transform
                .DOScale(1f, _config.OpenDuration)
                .SetEase(_config.OpenEase)
            );
            
            return sequency;
        }
    }
}