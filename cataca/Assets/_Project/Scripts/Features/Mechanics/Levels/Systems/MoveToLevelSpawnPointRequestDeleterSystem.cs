using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class MoveToLevelSpawnPointRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<MoveToLevelSpawnPointRequest> _moveToLevelSpawnPointRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<MoveToLevelSpawnPointRequest>()
                .End();
            
            _moveToLevelSpawnPointRequestPool = world.GetPool<MoveToLevelSpawnPointRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _moveToLevelSpawnPointRequestPool.Del(e);
        }
    }
}