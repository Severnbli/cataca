using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Audio.Configs;
using _Project.Scripts.Features.Audio.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioThemeVolumeSetupSystem : IEcsInitSystem, IEcsGameSystem
    {
        public AudioThemeVolumeSetupSystem(AudioConfig audioConfig)
        {
            _audioConfig = audioConfig;
        }
        
        private AudioConfig _audioConfig;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world
                .Filter<AudioSourceComponent>()
                .Inc<ThemeSourceMarker>()
                .End();
            
            var audioSourcePool = world.GetPool<AudioSourceComponent>();

            foreach (var e in filter)
            {
                ref var audioSource = ref audioSourcePool.Get(e);
                audioSource.AudioSource.volume = _audioConfig.ThemeVolume;
            }
        }
    }
}