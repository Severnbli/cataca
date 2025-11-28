using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class SpawnLevelFromStorageSystem : IEcsInitSystem, IEcsGameSystem
    {
        public SpawnLevelFromStorageSystem(BuiltInStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }
        
        private BuiltInStorageConfig _storageConfig;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var instantiateLevelRequestPool = world.GetPool<InstantiateLevelRequest>();

            var levelToLoad = StorageUtils.LoadLevelToLoad(_storageConfig);
            var entity = world.NewEntity();
            ref var instantiateLevelRequest = ref instantiateLevelRequestPool.Add(entity);
            instantiateLevelRequest.LevelDto = levelToLoad;
        }
    }
}