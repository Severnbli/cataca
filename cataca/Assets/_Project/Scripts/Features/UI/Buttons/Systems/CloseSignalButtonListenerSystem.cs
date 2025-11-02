using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Buttons.Systems
{
    public class CloseSignalButtonListenerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<CloseSignalComponent> _closeSignalPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .Inc<CloseSignalComponent>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
            _closeSignalPool = world.GetPool<CloseSignalComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                ref var closeSignal = ref _closeSignalPool.Get(e);

                closeSignal.RequestHandler.closeRequested = button.Button.Pressed;
            }
        }
    }
}