using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class JumpRequestSenderOnInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public JumpRequestSenderOnInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<JumpPerformRequest> _jumpPerformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ControlledByInputMarker>()
                .Inc<JumpComponent>()
                .End();
            
            _jumpPerformRequestPool = world.GetPool<JumpPerformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_inputService.Jump) return;
            
            foreach (var e in _filter)
            {
                if (_jumpPerformRequestPool.Has(e)) continue;
                
                _jumpPerformRequestPool.Add(e);
            }
        }
    }
}