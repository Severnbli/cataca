using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class RotatePlatformEngageRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<RotatePlatformEngageRequest> _rotationPlatformEngageRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RotatePlatformEngageRequest>()
                .End();
            
            _rotationPlatformEngageRequestPool = world.GetPool<RotatePlatformEngageRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _rotationPlatformEngageRequestPool.Del(e);
        }
    }
}