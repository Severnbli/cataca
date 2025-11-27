using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Services;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Systems
{
    public class CollisionCheckSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public CollisionCheckSystem(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }
        
        private CollisionService _collisionService;
        private EcsFilter _filter;
        private EcsPool<ColliderComponent> _colliderPool;
        
        private readonly List<Collider2D> _colliders = new();
        private readonly HashSet<Collider2D> _collidersSet = new();
        private readonly List<ColliderPair> _toRemove = new();
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ColliderComponent>()
                .End();

            _colliderPool = world.GetPool<ColliderComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            FetchColliders();
            CheckCollisions();
            ClearDictFromUnusedCollidersPairs();
        }

        private void CheckCollisions()
        {
            var dict = _collisionService.Collisions;

            for (var i = 0; i < _colliders.Count - 1; i++)
            {
                var colliderA = _colliders[i];

                for (var j = i + 1; j < _colliders.Count; j++)
                {
                    var colliderB = _colliders[j];
                    var pair = new ColliderPair(colliderA, colliderB);
                    dict.TryGetValue(pair, out var prevResult);

                    if (!Collider2DUtils.TryGetCollisionCheckResult(colliderA, colliderB, out var result, prevResult))
                    {
                        continue;
                    }
                    dict[pair] = result;
                }
            }
        }

        private void FetchColliders()
        {
            _colliders.Clear();
            _collidersSet.Clear();
            
            foreach (var e in _filter)
            {
                ref var collider = ref _colliderPool.Get(e);
                _colliders.Add(collider.Collider);
                _collidersSet.Add(collider.Collider);
            }
        }

        private void ClearDictFromUnusedCollidersPairs()
        {
            var dict = _collisionService.Collisions;
            _toRemove.Clear();

            foreach (var kbp in dict)
            {
                if (!_collidersSet.Contains(kbp.Key.A) || !_collidersSet.Contains(kbp.Key.B))
                {
                    _toRemove.Add(kbp.Key);
                }
            }

            foreach (var pair in _toRemove)
            {
                dict.Remove(pair);
            }
        }
    }
}