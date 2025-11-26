using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Requests;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Scenes.Systems
{
    public class ProcessSceneLoaderButtonPressedSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<SceneLoaderComponent> _sceneLoaderPool;
        private EcsPool<LoadSceneRequest> _loadSceneRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ButtonComponent>()
                .Inc<SceneLoaderComponent>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
            _sceneLoaderPool = world.GetPool<SceneLoaderComponent>();
            _loadSceneRequestPool = world.GetPool<LoadSceneRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                if (!button.Pressed) continue;
                
                ref var sceneLoader = ref _sceneLoaderPool.Get(e);
                ref var loadSceneRequest = ref _loadSceneRequestPool.AddComponentIfNotExists(e);
                
                loadSceneRequest.Scene = sceneLoader.Scene;
            }
        }
    }
}