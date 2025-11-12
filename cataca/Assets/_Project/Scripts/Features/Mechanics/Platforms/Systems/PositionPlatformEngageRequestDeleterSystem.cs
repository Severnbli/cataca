using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PositionPlatformEngageRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PositionPlatformEngageRequest> _positionPlatformEngageRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PositionPlatformEngageRequest>()
                .End();
            
            _positionPlatformEngageRequestPool = world.GetPool<PositionPlatformEngageRequest>();  
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _positionPlatformEngageRequestPool.Del(e);
        }
    }
}