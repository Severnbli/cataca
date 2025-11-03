using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI.Popups.Components;
using _Project.Scripts.Features.UI.Popups.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Popups.Systems
{
    public class PopupCloseListenerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<OpenCloseRequestHandlerComponent> _openCloseRequestHandlerPool;
        private EcsPool<PopupPlayCloseAnimRequest> _popupPlayCloseAnimRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PopupComponent>()
                .Inc<OpenCloseRequestHandlerComponent>()
                .Exc<PopupPlayCloseAnimRequest>()
                .End();
            
            _openCloseRequestHandlerPool = world.GetPool<OpenCloseRequestHandlerComponent>();
            _popupPlayCloseAnimRequestPool = world.GetPool<PopupPlayCloseAnimRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var openCloseRequestHandler = ref _openCloseRequestHandlerPool.Get(e);

                if (!openCloseRequestHandler.OpenCloseRequestHandler.closeRequested) continue;
                
                _popupPlayCloseAnimRequestPool.Add(e);
            }
        }
    }
}