using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Records.Markers;
using _Project.Scripts.Features.Mechanics.Records.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class RecordsPlayServiceUpdatePlayingStateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public RecordsPlayServiceUpdatePlayingStateSystem(RecordsPlayService recordsPlayService)
        {
            _recordsPlayService = recordsPlayService;
        }
        
        private RecordsPlayService _recordsPlayService;
        private EcsFilter _filter;
        private EcsPool<AudioSourceComponent> _audioSourcePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<AudioSourceComponent>()
                .Inc<RecordsPlayerMarker>()
                .End();
            
            _audioSourcePool = world.GetPool<AudioSourceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var audioSource = ref _audioSourcePool.Get(e);
                if (audioSource.AudioSource.isPlaying) return;
            }

            _recordsPlayService.Playing = false;
            _recordsPlayService.Current = null;
        }
    }
}