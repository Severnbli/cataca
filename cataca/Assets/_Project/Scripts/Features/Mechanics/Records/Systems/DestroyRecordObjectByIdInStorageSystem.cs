using System.Linq;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Components;
using _Project.Scripts.Features.Mechanics.Records.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class DestroyRecordObjectByIdInStorageSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public DestroyRecordObjectByIdInStorageSystem(BuiltInStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }
        
        private BuiltInStorageConfig _storageConfig;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<RecordObjectComponent> _recordObjectPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<RecordObjectComponent>()
                .End();
            
            _recordObjectPool = _world.GetPool<RecordObjectComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            var storageRecords = StorageUtils.LoadRecords(_storageConfig).Select(x => x.Id).ToHashSet();

            foreach (var e in _filter)
            {
                ref var recordObject = ref _recordObjectPool.Get(e);

                if (!storageRecords.Contains(recordObject.RecordObject.RecordId)) continue;
                
                Object.Destroy(recordObject.RecordObject.gameObject);
                _world.DelEntity(e);
            }
        }
    }
}