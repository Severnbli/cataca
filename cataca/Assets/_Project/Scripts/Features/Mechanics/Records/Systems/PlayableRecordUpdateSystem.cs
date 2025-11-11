using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Components;
using _Project.Scripts.Features.Audio.Configs;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class PlayableRecordUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PlayableRecordUpdateSystem(AudioConfig audioConfig, RecordsPlayService recordsPlayService)
        {
            _audioConfig = audioConfig;
            _recordsPlayService = recordsPlayService;
        }

        private AudioConfig _audioConfig;
        private RecordsPlayService _recordsPlayService;
        private EcsFilter _filter;
        private EcsPool<RecordComponent> _recordPool;
        private EcsPool<PlayableComponent> _playablePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RecordComponent>()
                .Inc<PlayableComponent>()
                .End();
            
            _recordPool = world.GetPool<RecordComponent>();
            _playablePool = world.GetPool<PlayableComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var record = ref _recordPool.Get(e);
                ref var playable = ref _playablePool.Get(e);
                
                if (_recordsPlayService.Current != record.Record || !_recordsPlayService.Playing)
                {
                    playable.Playable.sprite = _audioConfig.PlayableIsOff;
                    continue;
                }

                playable.Playable.sprite = _audioConfig.PlayableIsOn;
            }
        }
    }
}