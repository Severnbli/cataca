using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Markers;
using _Project.Scripts.Features.Data.Entities;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.UI.Sliders.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioSlidersLoaderSystem : IEcsInitSystem, IEcsGameSystem
    {
        public AudioSlidersLoaderSystem(BuiltInStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }

        private BuiltInStorageConfig _storageConfig;
        private EcsFilter _musicSlidersFilter;
        private EcsFilter _soundsSlidersFilter;
        private EcsPool<SliderComponent> _slidersPool;
        private Settings _settings;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _musicSlidersFilter = world
                .Filter<SliderComponent>()
                .Inc<MusicVolumeSetterMarker>()
                .End();

            _soundsSlidersFilter = world
                .Filter<SliderComponent>()
                .Inc<SoundsVolumeSetterMarker>()
                .End();
            
            _slidersPool = world.GetPool<SliderComponent>();
            
            _settings = StorageUtils.LoadSettings(_storageConfig);
            
            SetupMusicSliders();
            SetupSoundsSliders();
        }

        private void SetupMusicSliders()
        {
            var value = AudioUtils.DecibelsToLinear(_settings.MusicVolume);
            
            foreach (var e in _musicSlidersFilter)
            {
                ref var slider = ref _slidersPool.Get(e);
                slider.Slider.value = value;
            }
        }

        private void SetupSoundsSliders()
        {
            var value = AudioUtils.DecibelsToLinear(_settings.SoundsVolume);
            
            foreach (var e in _soundsSlidersFilter)
            {
                ref var slider = ref _slidersPool.Get(e);
                slider.Slider.value = value;
            }
        }
    }
}