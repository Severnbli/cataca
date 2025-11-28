using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PlatformControlledByInputUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<ControlledByInputMarker> _controlledByInputMarkerPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _controlledByInputMarkerPool = world.GetPool<ControlledByInputMarker>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var platform = _platformPool.Get(e);
                _controlledByInputMarkerPool.AddOrDelComponentOnCondition(e, () => platform.Platform.ControlledByInput);
            }
        }
    }
}