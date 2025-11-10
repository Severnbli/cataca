using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Features.Audio.Monos
{
    public class AudioPlayable : MonoBehaviour
    {
        [SerializeField] private Image _playable;
        
        public Image Playable => _playable;
    }
}