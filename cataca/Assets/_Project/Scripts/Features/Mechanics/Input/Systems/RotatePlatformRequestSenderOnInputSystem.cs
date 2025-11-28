using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Markers;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class RotatePlatformRequestSenderOnInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public RotatePlatformRequestSenderOnInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<RotatePlatformRequest> _rotatePlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ControlledByInputMarker>()
                .Inc<PlatformComponent>()
                .Inc<RotatePlatformMarker>()
                .End();
            
            _rotatePlatformRequestPool = world.GetPool<RotatePlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_inputService.Rotation) return;
            
            foreach (var e in _filter)
            {
                if (_rotatePlatformRequestPool.Has(e)) continue;
                
                _rotatePlatformRequestPool.Add(e);
            }
        }
    }
}