using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Scenes.Configs;
using _Project.Scripts.Features.Mechanics.Scenes.Requests;
using Leopotam.EcsLite;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Features.Mechanics.Scenes.Systems
{
    public class LoadSceneOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public LoadSceneOnRequestSystem(ScenesConfig scenesConfig)
        {
            _scenesConfig = scenesConfig;
        }
        
        private ScenesConfig _scenesConfig;
        private EcsFilter _filter;
        private EcsPool<LoadSceneRequest> _loadSceneRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadSceneRequest>()
                .End();
            
            _loadSceneRequestPool = world.GetPool<LoadSceneRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadSceneRequestPool.Get(e);
                if (!_scenesConfig.Scenes.TryGetValue(request.Scene, out var sceneName)) continue;

                SceneManager.LoadScene(sceneName);
                return;
            }
        }
    }
}