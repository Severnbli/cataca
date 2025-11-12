using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI.Popups.Components;
using _Project.Scripts.Features.UI.Popups.Configs;
using _Project.Scripts.Features.UI.Popups.Requests;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.UI.Popups.Systems
{
    public class PopupPlayCloseAnimRequestHandlerSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PopupPlayCloseAnimRequestHandlerSystem(PopupsAnimationConfig config)
        {
            _config = config;
        }
        
        private PopupsAnimationConfig _config;
        private EcsFilter _filter;
        private EcsPool<PopupPlayCloseAnimRequest> _popupPlayCloseAnimRequestPool;
        private EcsPool<PopupComponent> _popupPool;
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PopupPlayCloseAnimRequest>()
                .Inc<PopupComponent>()
                .End();
            
            _popupPlayCloseAnimRequestPool = world.GetPool<PopupPlayCloseAnimRequest>();
            _popupPool = world.GetPool<PopupComponent>();
            _tweenPool = world.GetPool<TweenComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                _popupPlayCloseAnimRequestPool.Del(e);
                
                ref var popup = ref _popupPool.Get(e);
                if (!popup.Opened) continue;
                
                popup.Opened = false;

                ref var tween = ref _tweenPool.Has(e)
                    ? ref _tweenPool.Get(e)
                    : ref _tweenPool.Add(e);
                
                tween.Tween?.Kill();
                tween.Tween = MakeAnim(popup);
            }
        }

        private Tween MakeAnim(PopupComponent popup)
        {
            var sequency = DOTween.Sequence();
            
            sequency.Append(popup.Fade
                .DOFade(
                _config.CloseFadeResultFactor / 255f, 
                _config.CloseDuration * _config.CloseFadeDurationPercentage)
            );
            
            sequency.Join(popup.Body.transform
                .DOScale(_config.CloseBodyScale, _config.CloseDuration)
                .SetEase(_config.CloseEase)
            );
            
            sequency.OnComplete(() => popup.Parent.SetActive(false));
            
            return sequency;
        }
    }
}