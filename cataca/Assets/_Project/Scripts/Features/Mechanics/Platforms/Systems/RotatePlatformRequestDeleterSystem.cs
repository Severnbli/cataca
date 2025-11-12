using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class RotatePlatformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<RotatePlatformRequest> _rotationPlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RotatePlatformRequest>()
                .End();
            
            _rotationPlatformRequestPool = world.GetPool<RotatePlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _rotationPlatformRequestPool.Del(e);
        }
    }
}