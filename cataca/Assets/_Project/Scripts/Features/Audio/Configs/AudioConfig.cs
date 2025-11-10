using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Audio.Configs
{
    public class AudioConfig : ScriptableObjectAutoInstaller<AudioConfig>
    {
        [SerializeField] private Sprite _playableIsOnSprite;
        [SerializeField] private Sprite _playableIsOffSprite;
        
        public Sprite PlayableIsOnSprite => _playableIsOnSprite;
        public Sprite PlayableIsOffSprite => _playableIsOffSprite;
    }
}