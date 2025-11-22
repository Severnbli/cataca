using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class SendDestroyLoadedLevelRequestOnInstantiateLevelRequestSystem : IEcsInitSystem, IEcsPostRunSystem, 
        IEcsGameSystem
    {
        public SendDestroyLoadedLevelRequestOnInstantiateLevelRequestSystem(LevelService levelService)
        {
            _levelService = levelService;
        }
        
        private LevelService _levelService;
        private EcsFilter _filter;
        private EcsPool<DestroyLoadedLevelRequest> _destroyLoadedLevelRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<InstantiateLevelRequest>()
                .End();
            
            _destroyLoadedLevelRequestPool = world.GetPool<DestroyLoadedLevelRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_levelService.Loaded && _filter.TryGetFirst(out var entity))
            {
                _destroyLoadedLevelRequestPool.Add(entity);
            }
        }
    }
}