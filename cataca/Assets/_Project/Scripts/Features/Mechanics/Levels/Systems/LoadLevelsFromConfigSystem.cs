using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Levels.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelsFromConfigSystem : IEcsPreInitSystem, IEcsGameSystem
    {
        public LoadLevelsFromConfigSystem(LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
        }
        
        private LevelsConfig _levelsConfig;
        
        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var levelPool = world.GetPool<LevelComponent>();

            var levels = _levelsConfig.Levels;
            foreach (var level in levels)
            {
                var entity = world.NewEntity();
                ref var levelComponent = ref levelPool.Add(entity);
                levelComponent = level;
            }
        }
    }
}