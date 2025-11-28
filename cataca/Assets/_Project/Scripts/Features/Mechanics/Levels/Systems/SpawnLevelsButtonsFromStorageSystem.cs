using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Configs;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class SpawnLevelsButtonsFromStorageSystem : IEcsPreInitSystem, IEcsGameSystem
    {
        public SpawnLevelsButtonsFromStorageSystem(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
        }
        
        private LevelsConfig _levelsConfig;
        
        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var instantiateLevelButtonRequestPool = world.GetPool<InstantiateLevelButtonRequest>();

            var levelsDto = _levelsConfig.Levels.Select(x => x.LevelDto).ToList();
            foreach (var levelDto in levelsDto)
            {
                var levelButtonEntity = world.NewEntity();
                ref var request = ref instantiateLevelButtonRequestPool.Add(levelButtonEntity);
                request.LevelDto = levelDto;
            }
        }
    }
}