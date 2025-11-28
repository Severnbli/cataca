using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Audio.Configs;
using _Project.Scripts.Features.Audio.Markers;
using _Project.Scripts.Features.Mechanics.Records.Markers;
using _Project.Scripts.Features.Mechanics.Records.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class RecordsPlayServiceUpdatePlayingStateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public RecordsPlayServiceUpdatePlayingStateSystem(RecordsPlayService recordsPlayService, AudioConfig audioConfig)
        {
            _recordsPlayService = recordsPlayService;
            _audioConfig = audioConfig;
        }
        
        private RecordsPlayService _recordsPlayService;
        private AudioConfig _audioConfig;
        private EcsFilter _recordsFilter;
        private EcsFilter _themesFilter;
        private EcsPool<AudioSourceComponent> _audioSourcePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _recordsFilter = world
                .Filter<AudioSourceComponent>()
                .Inc<RecordsPlayerMarker>()
                .End();

            _themesFilter = world
                .Filter<AudioSourceComponent>()
                .Inc<ThemeSourceMarker>()
                .End();
            
            _audioSourcePool = world.GetPool<AudioSourceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (IsRecordsPlaying())
            {
                foreach (var e in _themesFilter)
                {
                    ref var audioSource = ref _audioSourcePool.Get(e);
                    audioSource.AudioSource.volume = 0f;
                }
            }
            else
            {
                foreach (var e in _themesFilter)
                {
                    ref var audioSource = ref _audioSourcePool.Get(e);
                    audioSource.AudioSource.volume = _audioConfig.ThemeVolume;
                }
                
                _recordsPlayService.Playing = false;
                _recordsPlayService.Current = null;
            }
        }

        private bool IsRecordsPlaying()
        {
            foreach (var e in _recordsFilter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                if (audioSource.AudioSource.isPlaying) return true;
            }

            return false;
        }
    }
}