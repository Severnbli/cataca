using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Features.Audio.Monos
{
    public class AudioPlayable : MonoBehaviour
    {
        [SerializeField] private Image _playableIsOn;
        [SerializeField] private Image _playableIsOff;

        public Image PlayableIsOn => _playableIsOn;
        public Image PlayableIsOff => _playableIsOff;
    }
}