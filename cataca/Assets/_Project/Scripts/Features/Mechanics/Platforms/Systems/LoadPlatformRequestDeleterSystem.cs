using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class LoadPlatformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadPlatformRequest> _loadPlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadPlatformRequest>()
                .End();
            
            _loadPlatformRequestPool = world.GetPool<LoadPlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadPlatformRequestPool.Del(e);
        }
    }
}