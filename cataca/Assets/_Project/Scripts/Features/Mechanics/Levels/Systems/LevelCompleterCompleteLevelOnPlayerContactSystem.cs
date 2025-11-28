using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Enums;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Services;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LevelCompleterCompleteLevelOnPlayerContactSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public LevelCompleterCompleteLevelOnPlayerContactSystem(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }
        
        private CollisionService _collisionService;
        private EcsFilter _playersFilter;
        private EcsFilter _levelsCompletersFilter;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<SceneLoaderComponent> _sceneLoaderPool;
        private EcsPool<LoadSceneRequest> _loadSceneRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _playersFilter = world
                .Filter<ColliderComponent>()
                .Inc<PlayerMarker>()
                .End();

            _levelsCompletersFilter = world
                .Filter<ColliderComponent>()
                .Inc<LevelCompleterComponent>()
                .Inc<SceneLoaderComponent>()
                .Exc<LoadSceneRequest>()
                .End();
            
            _colliderPool = world.GetPool<ColliderComponent>();
            _sceneLoaderPool = world.GetPool<SceneLoaderComponent>();
            _loadSceneRequestPool = world.GetPool<LoadSceneRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerE in _playersFilter)
            {
                ref var playerCollider = ref _colliderPool.Get(playerE);

                foreach (var levelCompleterE in _levelsCompletersFilter)
                {
                    ref var levelCompleterCollider = ref _colliderPool.Get(levelCompleterE);
                    var pair = new ColliderPair(playerCollider.Collider, levelCompleterCollider.Collider);

                    if (!_collisionService.Collisions.TryGetValue(pair, out var collisionCheckResult) ||
                        collisionCheckResult.CheckStatus == CollisionCheckStatus.No)
                    {
                        continue;
                    }

                    ref var sceneLoader = ref _sceneLoaderPool.Get(levelCompleterE);
                    ref var loadSceneRequest = ref _loadSceneRequestPool.Add(levelCompleterE);
                    loadSceneRequest.Scene = sceneLoader.Scene;
                }
            }
        }
    }
}