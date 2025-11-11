using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Configs;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioSettingsLoaderSystem : IEcsInitSystem, IEcsGameSystem
    {
        public AudioSettingsLoaderSystem(AudioConfig audioConfig, BuiltInStorageConfig storageConfig)
        {
            _audioConfig = audioConfig;
            _storageConfig = storageConfig;
        }
        
        private AudioConfig _audioConfig;
        private BuiltInStorageConfig _storageConfig;
        
        public void Init(IEcsSystems systems)
        {
            var settings = StorageUtils.LoadSettings(_storageConfig);

            _audioConfig.AudioMixer.SetFloat(_audioConfig.GlobalVolume, settings.GlobalVolume);
            _audioConfig.AudioMixer.SetFloat(_audioConfig.MusicVolume, settings.MusicVolume);
            _audioConfig.AudioMixer.SetFloat(_audioConfig.SoundsVolume, settings.SoundsVolume);
        }
    }
}