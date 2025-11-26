using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Buttons.Systems
{
    public class ButtonUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                
                var pressed = button.Button.Pressed;

                button.Pressed = !button.WasPressed && pressed;
                button.Released = button.WasPressed && !pressed;
                button.Holding = pressed;

                button.WasPressed = pressed;
            }
        }
    }
}