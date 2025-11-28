using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class ProcessLevelButtonPressSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public ProcessLevelButtonPressSystem(BuiltInStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }
        
        private BuiltInStorageConfig _storageConfig;
        private EcsFilter _filter;
        private EcsPool<LevelButtonComponent> _levelButtonPool;
        private EcsPool<ButtonComponent> _buttonPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LevelButtonComponent>()
                .Inc<ButtonComponent>()
                .Inc<SceneLoaderComponent>()
                .End();
            
            _levelButtonPool = world.GetPool<LevelButtonComponent>();
            _buttonPool = world.GetPool<ButtonComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                if (!button.Pressed) continue;
                
                ref var levelButton = ref _levelButtonPool.Get(e);
                StorageUtils.SaveLevelToLoad(_storageConfig, levelButton.LevelButton.LevelDto);
            }
        }
    }
}