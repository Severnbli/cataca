using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class LoadRecordObjectOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadRecordObjectRequest> _loadRecordObjectRequestPool;
        private EcsPool<RecordObjectComponent> _recordObjectPool;
        private EcsPool<ColliderComponent> _colliderPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadRecordObjectRequest>()
                .End();
            
            _loadRecordObjectRequestPool = world.GetPool<LoadRecordObjectRequest>();
            _recordObjectPool = world.GetPool<RecordObjectComponent>();
            _colliderPool = world.GetPool<ColliderComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadRecordObjectRequestPool.Get(e);
                
                ref var recordObject = ref _recordObjectPool.AddComponentIfNotExists(e);
                recordObject.RecordObject = request.RecordObject;
                
                ref var collider = ref _colliderPool.AddComponentIfNotExists(e);
                collider.Collider = recordObject.RecordObject.Collider;
            }
        }
    }
}