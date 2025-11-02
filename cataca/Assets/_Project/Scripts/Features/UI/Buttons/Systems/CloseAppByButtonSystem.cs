using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.UI._Shared.Markers;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.UI.Buttons.Systems
{
    public class CloseAppByButtonSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .Inc<CloseAppSignalMarker>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);

                if (!button.Button.Pressed) continue;
                
                Application.Quit();
                return;
            }
        }
    }
}