using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI.Popups.Components;
using _Project.Scripts.Features.UI.Popups.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Popups.Systems
{
    public class PopupOpenListenerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<OpenCloseRequestHandlerComponent> _openCloseRequestHandlerPool;
        private EcsPool<PopupPlayOpenAnimRequest> _popupPlayOpenAnimRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PopupComponent>()
                .Inc<OpenCloseRequestHandlerComponent>()
                .Exc<PopupPlayOpenAnimRequest>()
                .End();
            
            _openCloseRequestHandlerPool = world.GetPool<OpenCloseRequestHandlerComponent>();
            _popupPlayOpenAnimRequestPool = world.GetPool<PopupPlayOpenAnimRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var openCloseRequestHandler = ref _openCloseRequestHandlerPool.Get(e);

                if (!openCloseRequestHandler.OpenCloseRequestHandler.openRequested) continue;
                
                _popupPlayOpenAnimRequestPool.Add(e);
            }
        }
    }
}