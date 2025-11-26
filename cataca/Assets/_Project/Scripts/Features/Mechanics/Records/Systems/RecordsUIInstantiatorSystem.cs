using System.Collections.Generic;
using System.Linq;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Components;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Configs;
using _Project.Scripts.Features.Mechanics.Records.Markers;
using _Project.Scripts.Features.Mechanics.Records.Monos;
using _Project.Scripts.Features.UI.Buttons.Components;
using _Project.Scripts.Features.UI.Containers.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class RecordsUIInstantiatorSystem : IEcsInitSystem, IEcsGameSystem
    {
        public RecordsUIInstantiatorSystem(RecordsUIInstantiatorConfig uiConfig, RecordsConfig recordsConfig, 
            BuiltInStorageConfig storageConfig)
        {
            _uiConfig = uiConfig;
            _recordsConfig = recordsConfig;
            _storageConfig = storageConfig;
        }

        private RecordsUIInstantiatorConfig _uiConfig;
        private RecordsConfig _recordsConfig;
        private BuiltInStorageConfig _storageConfig;
        private EcsWorld _world;
        private EcsFilter _containerFilter;
        private EcsPool<RecordComponent> _recordPool;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<ContainerComponent> _containerPool;
        private EcsPool<PlayableComponent> _playablePool;
        private List<RecordComponent> _recordsToSpawn = new();
        
        public void Init(IEcsSystems systems)
        {
            _recordsToSpawn = RecordsUtils.GetRecordComponentsByMemory(_recordsConfig.Records, _storageConfig);
            if (!_recordsToSpawn.Any()) return;
            
            _world = systems.GetWorld();
            
            _containerFilter = _world
                .Filter<ContainerComponent>()
                .Inc<RecordsContainerMarker>()
                .End();
            
            _recordPool = _world.GetPool<RecordComponent>();
            _buttonPool = _world.GetPool<ButtonComponent>();
            _containerPool = _world.GetPool<ContainerComponent>();
            _playablePool = _world.GetPool<PlayableComponent>();

            foreach (var e in _containerFilter)
            {
                ref var container = ref _containerPool.Get(e);
                SpawnRecordsAt(container.Parent.transform);
            }
        }
        
        private void SpawnRecordsAt(Transform parent)
        {
            foreach (var record in _recordsToSpawn)
            {
                var obj = Object.Instantiate(_uiConfig.Prefab, parent);
                var recordMono = obj.GetComponent<RecordButton>();
                if (recordMono is null) continue;

                recordMono.Icon.sprite = _uiConfig.Icon;
                    
                var entity = _world.NewEntity();
                
                ref var button = ref _buttonPool.Add(entity);
                button.Button = recordMono.Button;
                
                ref var recordComponent = ref _recordPool.Add(entity);
                recordComponent = record;
                
                ref var playable = ref _playablePool.Add(entity);
                playable.Playable = recordMono.Playable;
            }
        }
    }
}