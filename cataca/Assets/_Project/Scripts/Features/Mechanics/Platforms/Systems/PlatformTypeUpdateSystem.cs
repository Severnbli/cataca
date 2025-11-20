using System.Linq;
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
                ref var platform = ref _platformPool.Get(e);

                if (platform.Platform.PlatformTypes.Contains(PlatformType.Position))
                {
                    _positionPlatformMarkerPool.Add(e);
                }
                else
                {
                    _positionPlatformMarkerPool.Del(e);
                }

                if (platform.Platform.PlatformTypes.Contains(PlatformType.Rotate))
                {
                    _rotatePlatformMarkerPool.Add(e);
                }
                else
                {
                    _rotatePlatformMarkerPool.Del(e);
                }

                if (platform.Platform.PlatformTypes.Contains(PlatformType.Scale))
                {
                    _scalePlatformMarkerPool.Add(e);
                }
                else
                {
                    _scalePlatformMarkerPool.Del(e);
                }
            }
        }
    }
}