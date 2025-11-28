using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PositionPlatformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PositionPlatformRequest> _positionPlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PositionPlatformRequest>()
                .End();
            
            _positionPlatformRequestPool = world.GetPool<PositionPlatformRequest>();  
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _positionPlatformRequestPool.Del(e);
        }
    }
}