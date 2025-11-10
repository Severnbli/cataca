using System.Linq;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Data.Entities;
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
        private EcsPool<AudioSourceComponent> _audioSourcePool;
        private EcsPool<RecordPlayPauseRequest> _recordPlayPauseRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _audioSourcesFilter = world
                .Filter<AudioSourceComponent>()
                .Inc<RecordsPlayerMarker>()
                .End();

            _requestsFilter = world
                .Filter<RecordPlayPauseRequest>()
                .End();
            
            _audioSourcePool = world.GetPool<AudioSourceComponent>();
            _recordPlayPauseRequestPool = world.GetPool<RecordPlayPauseRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (!_requestsFilter.TryGetFirst(out var requestEntity) || _audioSourcesFilter.GetEntitiesCount() == 0) return;
            
            ref var recordPlayPauseRequest = ref _recordPlayPauseRequestPool.Get(requestEntity);

            if (recordPlayPauseRequest.Record == _recordsPlayService.Current)
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
            
            StartPlayback(recordPlayPauseRequest.Record);
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
            _recordsPlayService.Playing = false;
            
            foreach (var e in _audioSourcesFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                audioSource.AudioSource.UnPause();
            }
        }

        private void StartPlayback(Record record)
        {
            _recordsPlayService.Playing = true;
            if (!RecordsUtils.TryGetAudioClipByRecord(_recordsConfig.Records, record, out var audioClip)) return;

            foreach (var e in _audioSourcesFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                audioSource.AudioSource.clip = audioClip;
                audioSource.AudioSource.loop = _recordsConfig.LoopedPlayback;
                audioSource.AudioSource.Play();
            }
        }
    }
}