using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class InstantiateLevelButtonRequestDeleter : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<InstantiateLevelButtonRequest> _instantiateLevelButtonRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<InstantiateLevelButtonRequest>()
                .End();
            
            _instantiateLevelButtonRequestPool = world.GetPool<InstantiateLevelButtonRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _instantiateLevelButtonRequestPool.Del(e);
        }
    }
}