using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformAnimationConfig : ScriptableObjectAutoInstaller<PlatformAnimationConfig>
    {
        [SerializeField] private float duration;
        [SerializeField] private Ease ease = Ease.InOutQuad;
        
        public float Duration => duration;
        public Ease Ease => ease;
    }
}