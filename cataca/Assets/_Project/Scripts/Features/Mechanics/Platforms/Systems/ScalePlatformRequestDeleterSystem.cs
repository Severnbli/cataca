using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class ScalePlatformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ScalePlatformRequest> _scalePlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ScalePlatformRequest>()
                .End();
            
            _scalePlatformRequestPool = world.GetPool<ScalePlatformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _scalePlatformRequestPool.Del(e);
        }
    }
}