using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Configs;
using _Project.Scripts.Features.Audio.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioSettingsUpdateSystem : IEcsRunSystem, IEcsGameSystem
    {
        public AudioSettingsUpdateSystem(AudioConfig audioConfig, AudioService audioService)
        {
            _audioConfig = audioConfig;
            _audioService = audioService;
        }
        
        private AudioConfig _audioConfig;
        private AudioService _audioService;
        
        public void Run(IEcsSystems systems)
        {
            _audioConfig.AudioMixer.SetFloat(_audioConfig.GlobalVolume, _audioService.CurrentGlobalVolume);
            _audioConfig.AudioMixer.SetFloat(_audioConfig.MusicVolume, _audioService.CurrentMusicVolume);
            _audioConfig.AudioMixer.SetFloat(_audioConfig.SoundsVolume, _audioService.CurrentSoundsVolume);
        }
    }
}