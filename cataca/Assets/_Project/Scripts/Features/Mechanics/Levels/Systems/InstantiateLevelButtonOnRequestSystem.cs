using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Levels.Configs;
using _Project.Scripts.Features.Mechanics.Levels.Markers;
using _Project.Scripts.Features.Mechanics.Levels.Monos;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.UI.Containers.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class InstantiateLevelButtonOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public InstantiateLevelButtonOnRequestSystem(LevelsUIInstantiatorConfig uiConfig)
        {
            _uiConfig = uiConfig;
        }
        
        private LevelsUIInstantiatorConfig _uiConfig;
        private EcsWorld _world;
        private EcsFilter _requestsFilter;
        private EcsFilter _levelButtonFilter;
        private EcsFilter _levelsUIContainerFilter;
        private EcsPool<InstantiateLevelButtonRequest> _instantiateLevelButtonRequestPool;
        private EcsPool<LoadLevelButtonRequest> _loadLevelButtonRequestPool;
        private EcsPool<LevelButtonComponent> _levelButtonPool;
        private EcsPool<ContainerComponent> _containerPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestsFilter = _world
                .Filter<InstantiateLevelButtonRequest>()
                .End();

            _levelButtonFilter = _world
                .Filter<LevelButtonComponent>()
                .End();

            _levelsUIContainerFilter = _world
                .Filter<ContainerComponent>()
                .Inc<LevelsUIContainerMarker>()
                .End();
            
            _instantiateLevelButtonRequestPool = _world.GetPool<InstantiateLevelButtonRequest>();
            _loadLevelButtonRequestPool = _world.GetPool<LoadLevelButtonRequest>();
            _levelButtonPool = _world.GetPool<LevelButtonComponent>();
            _containerPool = _world.GetPool<ContainerComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var requestEntity in _requestsFilter)
            {
                ref var request = ref _instantiateLevelButtonRequestPool.Get(requestEntity);
                if (IsLevelButtonExists(ref request)) continue;
                
                InstantiateLevelButton(ref request);
            }
        }

        private bool IsLevelButtonExists(ref InstantiateLevelButtonRequest request)
        {
            foreach (var levelButtonEntity in _levelButtonFilter)
            {
                ref var levelButton = ref _levelButtonPool.Get(levelButtonEntity);
                if (request.LevelDto == levelButton.LevelButton.LevelDto) return true;
            }
            
            return false;
        }

        private void InstantiateLevelButton(ref InstantiateLevelButtonRequest request)
        {
            foreach (var containerEntity in _levelsUIContainerFilter)
            {
                ref var container = ref _containerPool.Get(containerEntity);

                var levelButtonObject = Object.Instantiate(_uiConfig.Prefab, container.Parent.transform);
                var levelButton = levelButtonObject.GetComponent<LevelButton>();

                if (levelButton == null) continue;
                
                var levelButtonEntity = _world.NewEntity();
                ref var loadLevelButtonRequest = ref _loadLevelButtonRequestPool.Add(levelButtonEntity);
                loadLevelButtonRequest.LevelButton = levelButton;
            }
        }
    }
}