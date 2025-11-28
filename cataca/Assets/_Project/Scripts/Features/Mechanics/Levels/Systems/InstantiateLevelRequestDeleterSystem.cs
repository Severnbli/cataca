using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class InstantiateLevelRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<InstantiateLevelRequest> _instantiateLevelRequestsPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<InstantiateLevelRequest>()
                .End();
            
            _instantiateLevelRequestsPool = world.GetPool<InstantiateLevelRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _instantiateLevelRequestsPool.Del(e);
        }
    }
}