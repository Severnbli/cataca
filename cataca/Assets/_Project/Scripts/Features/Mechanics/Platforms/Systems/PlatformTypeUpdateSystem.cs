using System.Linq;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Enums;
using _Project.Scripts.Features.Mechanics.Platforms.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PlatformTypeUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlatformComponent> _platformPool;
        private EcsPool<PositionPlatformMarker> _positionPlatformMarkerPool;
        private EcsPool<RotatePlatformMarker> _rotatePlatformMarkerPool;
        private EcsPool<ScalePlatformMarker> _scalePlatformMarkerPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlatformComponent>()
                .End();
            
            _platformPool = world.GetPool<PlatformComponent>();
            _positionPlatformMarkerPool = world.GetPool<PositionPlatformMarker>();
            _rotatePlatformMarkerPool = world.GetPool<RotatePlatformMarker>();
            _scalePlatformMarkerPool = world.GetPool<ScalePlatformMarker>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var platform = _platformPool.Get(e);

                _positionPlatformMarkerPool.AddOrDelComponentOnCondition(e,
                    () => platform.Platform.PlatformTypes.Contains(PlatformType.Position));

                _rotatePlatformMarkerPool.AddOrDelComponentOnCondition(e,
                    () => platform.Platform.PlatformTypes.Contains(PlatformType.Rotate));

                _scalePlatformMarkerPool.AddOrDelComponentOnCondition(e,
                    () => platform.Platform.PlatformTypes.Contains(PlatformType.Scale));
            }
        }
    }
}