using System.Linq;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Enums;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Services;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class PlayerPickupRecordObjectSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public PlayerPickupRecordObjectSystem(CollisionService collisionService, BuiltInStorageConfig storageConfig,
            RecordsConfig recordsConfig)
        {
            _collisionService = collisionService;
            _storageConfig = storageConfig;
            _recordsConfig = recordsConfig;
        }
        
        private CollisionService _collisionService;
        private BuiltInStorageConfig _storageConfig;
        private RecordsConfig _recordsConfig;
        private EcsFilter _playersFilter;
        private EcsFilter _recordsFilter;
        private EcsPool<ColliderComponent> _colliderPool;
        private EcsPool<RecordObjectComponent> _recordObjectPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _playersFilter = world
                .Filter<ColliderComponent>()
                .Inc<PlayerMarker>()
                .End();

            _recordsFilter = world
                .Filter<ColliderComponent>()
                .Inc<RecordObjectComponent>()
                .End();
            
            _colliderPool = world.GetPool<ColliderComponent>();
            _recordObjectPool = world.GetPool<RecordObjectComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerE in _playersFilter)
            {
                ref var playerCollider = ref _colliderPool.Get(playerE);
                
                foreach (var recordsE in _recordsFilter)
                {
                    ref var recordCollider = ref _colliderPool.Get(recordsE);
                    
                    var pair = new ColliderPair(playerCollider.Collider, recordCollider.Collider);
                    if (!_collisionService.Collisions.TryGetValue(pair, out var collisionCheckResult) ||
                        collisionCheckResult.CheckStatus == CollisionCheckStatus.No)
                    {
                        continue;
                    }
                    
                    var recordObject = _recordObjectPool.Get(recordsE);
                    var recordDto = _recordsConfig.Records
                        .Select(x => x.RecordDto)
                        .FirstOrDefault(x => x.Id == recordObject.RecordObject.RecordId);
                    
                    StorageUtils.AddRecord(_storageConfig, recordDto);
                }
            }
        }
    }
}