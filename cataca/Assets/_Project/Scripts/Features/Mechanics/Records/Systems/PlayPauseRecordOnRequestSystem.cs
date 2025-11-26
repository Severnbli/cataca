using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Configs;
using _Project.Scripts.Features.Mechanics.Records.Markers;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using _Project.Scripts.Features.Mechanics.Records.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class PlayPauseRecordOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PlayPauseRecordOnRequestSystem(RecordsPlayService recordsPlayService, RecordsConfig recordsConfig)
        {
            _recordsPlayService = recordsPlayService;
            _recordsConfig = recordsConfig;
        }
        
        private RecordsPlayService _recordsPlayService;
        private RecordsConfig _recordsConfig;
        private EcsFilter _audioSourcesFilter;
        private EcsFilter _requestsFilter;
        private EcsPool<RecordComponent> _recordPool;
        private EcsPool<AudioSourceComponent> _audioSourcePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _audioSourcesFilter = world
                .Filter<AudioSourceComponent>()
                .Inc<RecordsPlayerMarker>()
                .End();

            _requestsFilter = world
                .Filter<RecordPlayPauseRequest>()
                .Inc<RecordComponent>()
                .End();
            
            _audioSourcePool = world.GetPool<AudioSourceComponent>();
            _recordPool = world.GetPool<RecordComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (!_requestsFilter.TryGetFirst(out var requestEntity) || _audioSourcesFilter.GetEntitiesCount() == 0) return;
            
            ref var record = ref _recordPool.Get(requestEntity);

            if (record.recordDto == _recordsPlayService.Current)
            {
                if (_recordsPlayService.Playing)
                {
                    PausePlayback();
                }
                else
                {
                    UnpausePlayback();
                }
                
                return;
            }
            
            StartPlayback(record);
        }

        private void PausePlayback()
        {
            _recordsPlayService.Playing = false;
            
            foreach (var e in _audioSourcesFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                audioSource.AudioSource.Pause();
            }
        }

        private void UnpausePlayback()
        {
            _recordsPlayService.Playing = true;
            
            foreach (var e in _audioSourcesFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                audioSource.AudioSource.UnPause();
            }
        }

        private void StartPlayback(RecordComponent record)
        {
            _recordsPlayService.Playing = true;
            _recordsPlayService.Current = record.recordDto;

            foreach (var e in _audioSourcesFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                audioSource.AudioSource.clip = record.AudioClip;
                audioSource.AudioSource.loop = _recordsConfig.LoopedPlayback;
                audioSource.AudioSource.Play();
            }
        }
    }
}