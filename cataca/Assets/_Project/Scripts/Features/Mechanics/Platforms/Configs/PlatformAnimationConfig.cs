using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformAnimationConfig : ScriptableObjectAutoInstaller<PlatformAnimationConfig>
    {
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.InOutQuad;
        [SerializeField] private float _transitionDuration = 0.2f;
        
        public float Duration => _duration;
        public Ease Ease => _ease;
        public float TransitionDuration => _transitionDuration;
    }
}