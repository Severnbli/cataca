using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Services;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioSettingsWriterSystem : IEcsDestroySystem, IEcsGameSystem
    {
        public AudioSettingsWriterSystem(AudioService audioService, BuiltInStorageConfig storageConfig)
        {
            _audioService = audioService;
            _storageConfig = storageConfig;
        }
        
        private AudioService _audioService;
        private BuiltInStorageConfig _storageConfig;
        
        public void Destroy(IEcsSystems systems)
        {
            var settings = StorageUtils.LoadSettings(_storageConfig);
            
            settings.GlobalVolume = _audioService.CurrentGlobalVolume;
            settings.MusicVolume = _audioService.CurrentMusicVolume;
            settings.SoundsVolume = _audioService.CurrentSoundsVolume;
            
            StorageUtils.SaveSettings(_storageConfig, settings);
        }
    }
}