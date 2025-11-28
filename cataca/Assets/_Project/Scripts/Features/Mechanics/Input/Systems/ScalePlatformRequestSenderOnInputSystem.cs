using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Markers;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class ScalePlatformRequestSenderOnInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public ScalePlatformRequestSenderOnInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<ScalePlatformRequest> _scalePlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ControlledByInputMarker>()
                .Inc<PlatformComponent>()
                .Inc<ScalePlatformMarker>()
                .End();
            
            _scalePlatformRequestPool = world.GetPool<ScalePlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (!_inputService.Scale) return;

            foreach (var e in _filter)
            {
                if (_scalePlatformRequestPool.Has(e)) continue;
                
                _scalePlatformRequestPool.Add(e);
            }
        }
    }
}