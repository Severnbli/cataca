using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Audio.Configs
{
    public class AudioConfig : ScriptableObjectAutoInstaller<AudioConfig>
    {
        [SerializeField] private Sprite _playableIsOn;
        [SerializeField] private Sprite _playableIsOff;
        
        public Sprite PlayableIsOn => _playableIsOn;
        public Sprite PlayableIsOff => _playableIsOff;
    }
}