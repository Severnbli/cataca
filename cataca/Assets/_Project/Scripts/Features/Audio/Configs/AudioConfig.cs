using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Features.Audio.Configs
{
    public class AudioConfig : ScriptableObjectAutoInstaller<AudioConfig>
    {
        [SerializeField] private Sprite _playableIsOn;
        [SerializeField] private Sprite _playableIsOff;
        [SerializeField] private AudioMixerGroup _globalMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        [SerializeField] private AudioMixerGroup _soundsMixerGroup;
        
        public Sprite PlayableIsOn => _playableIsOn;
        public Sprite PlayableIsOff => _playableIsOff;
        public AudioMixerGroup GlobalMixerGroup => _globalMixerGroup;
        public AudioMixerGroup MusicMixerGroup => _musicMixerGroup;
        public AudioMixerGroup SoundsMixerGroup => _soundsMixerGroup;
    }
}