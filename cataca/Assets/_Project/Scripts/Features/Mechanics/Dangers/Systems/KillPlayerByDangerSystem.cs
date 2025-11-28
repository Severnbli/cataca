using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Enums;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Services;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Dangers.Systems
{
    public class KillPlayerByDangerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public KillPlayerByDangerSystem(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }
        
        private CollisionService _collisionService;
        private EcsFilter _playersFilter;
        private EcsFilter _dangersFilter;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<KillPlayerRequest> _killPlayerRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _playersFilter = world
                .Filter<ColliderComponent>()
                .Inc<PlayerMarker>()
                .End();

            _dangersFilter = world
                .Filter<ColliderComponent>()
                .Inc<ColliderComponent>()
                .End();
            
            _colliderPool = world.GetPool<ColliderComponent>();
            _killPlayerRequestPool = world.GetPool<KillPlayerRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerE in _playersFilter)
            {
                ref var playerCollider = ref _colliderPool.Get(playerE);

                foreach (var dangerE in _dangersFilter)
                {
                    ref var dangerCollider = ref _colliderPool.Get(dangerE);
                    
                    var pair = new ColliderPair(playerCollider.Collider, dangerCollider.Collider);
                    if (!_collisionService.Collisions.TryGetValue(pair, out var collisionCheckResult) ||
                        collisionCheckResult.CheckStatus == CollisionCheckStatus.No)
                    {
                        continue;
                    }

                    _killPlayerRequestPool.AddComponentIfNotExists(playerE);
                }
            }
        }
    }
}