using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Markers;
using _Project.Scripts.Features._Shared.Requests;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.UI.Buttons.Systems
{
    public class CloseAppSignalButtonListenerSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<CloseAppRequest> _closeAppRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .Inc<CloseAppSignalMarker>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
            _closeAppRequestPool = world.GetPool<CloseAppRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);

                if (!button.Pressed) continue;
                
                var world = systems.GetWorld();
                var requestEntity = world.NewEntity();
                _closeAppRequestPool.Add(requestEntity);
            }
        }
    }
}