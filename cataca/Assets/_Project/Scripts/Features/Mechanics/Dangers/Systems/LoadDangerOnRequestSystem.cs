using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Dangers.Components;
using _Project.Scripts.Features.Mechanics.Dangers.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Markers;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Dangers.Systems
{
    public class LoadDangerOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadDangerRequest> _loadDangerRequestPool;
        private EcsPool<DangerComponent> _dangerPool;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<DelEntityOnDestroyLevelMarker> _delEntityOnDestroyLevelMarkerPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadDangerRequest>()
                .End();
            
            _loadDangerRequestPool = world.GetPool<LoadDangerRequest>();
            _dangerPool = world.GetPool<DangerComponent>();
            _colliderPool = world.GetPool<ColliderComponent>();
            _delEntityOnDestroyLevelMarkerPool = world.GetPool<DelEntityOnDestroyLevelMarker>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadDangerRequestPool.Get(e);

                ref var danger = ref _dangerPool.AddComponentIfNotExists(e);
                danger.Danger = request.Danger;
                
                ref var collider = ref _colliderPool.AddComponentIfNotExists(e);
                collider.Collider = request.Danger.Collider;

                _delEntityOnDestroyLevelMarkerPool.AddComponentIfNotExists(e);
            }
        }
    }
}