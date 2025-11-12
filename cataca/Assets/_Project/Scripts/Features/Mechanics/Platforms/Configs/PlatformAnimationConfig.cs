using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformAnimationConfig : ScriptableObjectAutoInstaller<PlatformAnimationConfig>
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private Ease ease = Ease.InOutQuad;
        [SerializeField] private float transitionDuration = 0.2f;
        
        public float Duration => duration;
        public Ease Ease => ease;
        public float TransitionDuration => transitionDuration;
    }
}