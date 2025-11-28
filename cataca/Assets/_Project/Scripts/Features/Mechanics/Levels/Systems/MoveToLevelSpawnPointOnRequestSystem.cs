using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class MoveToLevelSpawnPointOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public MoveToLevelSpawnPointOnRequestSystem(LevelService levelService)
        {
            _levelService = levelService;
        }
        
        private LevelService _levelService;
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<MoveToLevelSpawnPointRequest>()
                .Inc<TransformComponent>()
                .End();
            
            _transformPool = world.GetPool<TransformComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (!_levelService.Loaded || _levelService.LoadedLevel == null) return;

            var levelSpawnPoint = _levelService.LoadedLevel.SpawnPoint.position;
            
            foreach (var e in _filter)
            {
                ref var transform = ref _transformPool.Get(e);
                transform.Transform.position = levelSpawnPoint;
            }
        }
    }
}