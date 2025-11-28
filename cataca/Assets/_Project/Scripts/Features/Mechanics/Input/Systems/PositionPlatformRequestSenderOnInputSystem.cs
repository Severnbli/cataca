using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Markers;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class PositionPlatformRequestSenderOnInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public PositionPlatformRequestSenderOnInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<PositionPlatformRequest> _positionPlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ControlledByInputMarker>()
                .Inc<PlatformComponent>()
                .Inc<PositionPlatformMarker>()
                .End();
            
            _positionPlatformRequestPool = world.GetPool<PositionPlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_inputService.Position) return;

            foreach (var e in _filter)
            {
                if (_positionPlatformRequestPool.Has(e)) continue;
                
                _positionPlatformRequestPool.Add(e);
            }
        }
    }
}