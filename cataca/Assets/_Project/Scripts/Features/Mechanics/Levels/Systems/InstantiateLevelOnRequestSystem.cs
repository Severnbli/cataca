using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Levels.Markers;
using _Project.Scripts.Features.Mechanics.Levels.Monos;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class InstantiateLevelOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public InstantiateLevelOnRequestSystem(LevelService levelService)
        {
            _levelService = levelService;
        }
        
        private LevelService _levelService;
        private EcsWorld _world;
        private EcsFilter _requestsFilter;
        private EcsFilter _levelsFilter;
        private EcsFilter _levelsContainerFilter;
        private EcsPool<InstantiateLevelRequest> _instantiateLevelRequestPool;
        private EcsPool<LevelComponent> _levelsPool;
        private EcsPool<LoadLevelRequest> _loadLevelRequestPool;
        private EcsPool<TransformComponent> _transformPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestsFilter = _world
                .Filter<InstantiateLevelRequest>()
                .End();

            _levelsFilter = _world
                .Filter<LevelComponent>()
                .End();

            _levelsContainerFilter = _world
                .Filter<LevelsContainerMarker>()
                .Inc<TransformComponent>()
                .End();
            
            _instantiateLevelRequestPool = _world.GetPool<InstantiateLevelRequest>();
            _levelsPool = _world.GetPool<LevelComponent>();
            _loadLevelRequestPool = _world.GetPool<LoadLevelRequest>();
            _transformPool = _world.GetPool<TransformComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_levelService.Loaded) return;
            
            foreach (var requestEntity in _requestsFilter)
            {
                var instantiateLevelRequest = _instantiateLevelRequestPool.Get(requestEntity);

                if (TrySpawnLevel(instantiateLevelRequest)) return;
            }
        }

        private bool TrySpawnLevel(InstantiateLevelRequest instantiateLevelRequest)
        {
            foreach (var levelEntity in _levelsFilter)
            {
                var levelComponent = _levelsPool.Get(levelEntity);

                if (levelComponent.LevelDto != instantiateLevelRequest.LevelDto) continue;

                SpawnLevel(levelComponent);
                return true;
            }
            
            return false;
        }

        private void SpawnLevel(LevelComponent levelComponent)
        {
            Transform parent = null;

            if (_levelsContainerFilter.TryGetFirst(out var levelsContainerEntity))
            {
                ref var levelsContainerTransform = ref _transformPool.Get(levelsContainerEntity);
                parent = levelsContainerTransform.Transform;
            }
            
            var levelObject = Object.Instantiate(levelComponent.Prefab, parent);
            var level = levelObject.GetComponent<Level>();
            
            if (level is not null) SendLoadRequest(level);

            _levelService.Loaded = true;
            _levelService.LevelDto = levelComponent.LevelDto;
            _levelService.LoadedLevel = level;
        }

        private void SendLoadRequest(Level level)
        {
            var entity = _world.NewEntity();
            ref var loadLevelRequest = ref _loadLevelRequestPool.Add(entity);
            loadLevelRequest.Level = level;
        }
    }
}