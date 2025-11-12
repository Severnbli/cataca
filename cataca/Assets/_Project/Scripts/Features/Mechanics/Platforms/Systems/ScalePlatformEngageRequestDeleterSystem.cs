using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class ScalePlatformEngageRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ScalePlatformEngageRequest> _scalePlatformEngageRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ScalePlatformEngageRequest>()
                .End();
            
            _scalePlatformEngageRequestPool = world.GetPool<ScalePlatformEngageRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _scalePlatformEngageRequestPool.Del(e);
        }
    }
}