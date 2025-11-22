using _Project.Scripts.Core.Systems.Interfaces;
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
        
        private LevelService _levelService;
        private EcsFilter _filter;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<DestroyLoadedLevelRequest>()
                .End();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_filter.GetEntitiesCount() == 0) return;
            
            if (_levelService.LoadedLevel != null) Object.Destroy(_levelService.LoadedLevel);

            _levelService.Loaded = false;
        }
    }
}