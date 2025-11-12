using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class InstantiatePlatformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<InstantiatePlatformRequest> _instantiatePlatformRequestsPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<InstantiatePlatformRequest>()
                .End();
            
            _instantiatePlatformRequestsPool = world.GetPool<InstantiatePlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _instantiatePlatformRequestsPool.Del(e);
        }
    }
}