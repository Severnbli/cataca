using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformAnimationConfig : ScriptableObjectAutoInstaller<PlatformAnimationConfig>
    {
        [Header("Transform")]
        
        [SerializeField] private float _transformDuration = 0.2f;
        [SerializeField] private Ease _transformEase = Ease.InOutQuad;
        [SerializeField] private float _transformTransitionDuration = 0.2f;
        
        public float TransformDuration => _transformDuration;
        public Ease TransformEase => _transformEase;
        public float TransformTransitionDuration => _transformTransitionDuration;
    }
}