using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class DashRequestSenderOnInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public DashRequestSenderOnInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<DashPerformRequest> _dashPerformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ControlledByInputMarker>()
                .Inc<DashComponent>()
                .End();
            
            _dashPerformRequestPool = world.GetPool<DashPerformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_inputService.Dash) return;

            foreach (var e in _filter)
            {
                if (_dashPerformRequestPool.Has(e)) continue;
                
                _dashPerformRequestPool.Add(e);
            }
        }
    }
}