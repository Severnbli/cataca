using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features._Shared.Monos;
using _Project.Scripts.Features.UI.Popups.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Popups.Systems
{
    public class PopupCloseListenerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PopupComponent> _popupPool;
        private EcsPool<OpenCloseRequestHandlerComponent> _openCloseRequestHandlerPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PopupComponent>()
                .Inc<OpenCloseRequestHandlerComponent>()
                .End();
            
            _popupPool = world.GetPool<PopupComponent>();
            _openCloseRequestHandlerPool = world.GetPool<OpenCloseRequestHandlerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var openCloseRequestHandler = ref _openCloseRequestHandlerPool.Get(e);

                if (!openCloseRequestHandler.OpenCloseRequestHandler.closeRequested) continue;
                
                // TODO : anim & close popup
            }
        }
    }
}