using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Markers;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class DestroyLoadedLevelOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public DestroyLoadedLevelOnRequestSystem(LevelService levelService)
        {
            _levelService = levelService;
        }

        private EcsWorld _world;
        private EcsFilter _requestsFilter;
        private EcsFilter _delEntityFilter;
        private LevelService _levelService;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestsFilter = _world
                .Filter<DestroyLoadedLevelRequest>()
                .End();

            _delEntityFilter = _world
                .Filter<DelEntityOnDestroyLevelMarker>()
                .End();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_requestsFilter.GetEntitiesCount() == 0) return;

            foreach (var e in _delEntityFilter)
            {
                _world.DelEntity(e);
            }
            
            if (_levelService.LoadedLevel != null) Object.Destroy(_levelService.LoadedLevel);

            _levelService.Loaded = false;
        }
    }
}