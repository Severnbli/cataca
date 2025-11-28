using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Levels.Markers;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Enums;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelCompleterOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadLevelCompleterRequest> _loadLevelCompleterRequestPool;
        private EcsPool<LevelCompleterComponent> _levelCompleterPool;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<SceneLoaderComponent> _sceneLoaderPool;
        private EcsPool<DelEntityOnDestroyLevelMarker> _delEntityOnDestroyMarkerPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadLevelCompleterRequest>()
                .End();
            
            _loadLevelCompleterRequestPool = world.GetPool<LoadLevelCompleterRequest>();
            _levelCompleterPool = world.GetPool<LevelCompleterComponent>();
            _colliderPool = world.GetPool<ColliderComponent>();
            _sceneLoaderPool = world.GetPool<SceneLoaderComponent>();
            _delEntityOnDestroyMarkerPool = world.GetPool<DelEntityOnDestroyLevelMarker>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadLevelCompleterRequestPool.Get(e);
                
                ref var levelCompleter = ref _levelCompleterPool.AddComponentIfNotExists(e);
                levelCompleter.LevelCompleter = request.LevelCompleter;
                
                ref var collider = ref _colliderPool.AddComponentIfNotExists(e);
                collider.Collider = request.LevelCompleter.Collider;
                
                ref var sceneLoader = ref _sceneLoaderPool.AddComponentIfNotExists(e);
                sceneLoader.Scene = Scene.Menu;

                _delEntityOnDestroyMarkerPool.AddComponentIfNotExists(e);
            }
        }
    }
}