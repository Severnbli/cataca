using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Services;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioServiceLoaderSystem : IEcsInitSystem, IEcsGameSystem
    {
        public AudioServiceLoaderSystem(AudioService audioService, BuiltInStorageConfig storageConfig)
        {
            _audioService = audioService;
            _storageConfig = storageConfig;
        }
        
        private AudioService _audioService;
        private BuiltInStorageConfig _storageConfig;
        
        public void Init(IEcsSystems systems)
        {
            var settings = StorageUtils.LoadSettings(_storageConfig);

            _audioService.CurrentGlobalVolume = settings.GlobalVolume;
            _audioService.CurrentMusicVolume = settings.MusicVolume;
            _audioService.CurrentSoundsVolume = settings.SoundsVolume;
        }
    }
}