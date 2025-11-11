using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Audio.Markers;
using _Project.Scripts.Features.Audio.Services;
using _Project.Scripts.Features.UI.Sliders.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Audio.Systems
{
    public class AudioServiceUpdateOnSlidersChangeValueSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public AudioServiceUpdateOnSlidersChangeValueSystem(AudioService audioService)
        {
            _audioService = audioService;
        }
        
        private AudioService _audioService;
        private EcsFilter _musicSlidersFilter;
        private EcsFilter _soundsSlidersFilter;
        private EcsPool<SliderComponent> _sliderPool;
        
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
            
            _sliderPool = world.GetPool<SliderComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            SearchForUpdates();
            UpdateSliders();
        }

        private void SearchForUpdates()
        {
            foreach (var e in _musicSlidersFilter)
            {
                ref var slider = ref _sliderPool.Get(e);
                
                var decibels = AudioUtils.LinearToDecibels(slider.Slider.value);
                if (Mathf.Approximately(_audioService.CurrentMusicVolume, decibels)) continue;
                
                _audioService.CurrentMusicVolume = decibels;
                break;
            }

            foreach (var e in _soundsSlidersFilter)
            {
                ref var slider = ref _sliderPool.Get(e);
                
                var decibels = AudioUtils.LinearToDecibels(slider.Slider.value);
                if (Mathf.Approximately(_audioService.CurrentSoundsVolume, decibels)) continue;
                
                _audioService.CurrentSoundsVolume = decibels;
                break;
            }
        }

        private void UpdateSliders()
        {
            var musicValue = AudioUtils.DecibelsToLinear(_audioService.CurrentMusicVolume);
            foreach (var e in _musicSlidersFilter)
            {
                ref var slider = ref _sliderPool.Get(e);
                slider.Slider.value = musicValue;
            }

            var soundsValue = AudioUtils.DecibelsToLinear(_audioService.CurrentSoundsVolume);
            foreach (var e in _soundsSlidersFilter)
            {
                ref var slider = ref _sliderPool.Get(e);
                slider.Slider.value = soundsValue;
            }
        }
    }
}