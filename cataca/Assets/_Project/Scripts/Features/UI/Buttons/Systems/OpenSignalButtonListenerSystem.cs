using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Buttons.Systems
{
    public class OpenSignalButtonListenerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<OpenSignalComponent> _openSignalPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .Inc<OpenSignalComponent>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
            _openSignalPool = world.GetPool<OpenSignalComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                ref var openSignal = ref _openSignalPool.Get(e);

                openSignal.RequestHandler.openRequested = button.Pressed;
            }
        }
    }
}