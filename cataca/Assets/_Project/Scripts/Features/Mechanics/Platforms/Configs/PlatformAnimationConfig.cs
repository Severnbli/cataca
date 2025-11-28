using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformAnimationConfig : ScriptableObjectAutoInstaller<PlatformAnimationConfig>
    {
        [Header("Transform")]
        
        [SerializeField] private float _positionTransitionSpeed = 6f;
        [SerializeField] private float _rotateTransitionSpeed = 60f;
        [SerializeField] private float _scaleTransitionSpeed = 6f;
        [SerializeField] private Ease _transformEase = Ease.InOutQuad;
        [SerializeField] private float _transformTransitionDuration = 0.2f;
        
        [Header("Color")]
        
        [SerializeField] private Color _baseColor = Color.white;
        [SerializeField] private Color _positionColor = Color.cyan;
        [SerializeField] private Color _rotateColor = Color.green;
        [SerializeField] private Color _scaleColor = Color.yellow;
        [SerializeField] private Ease _colorEase = Ease.OutQuint;
        [SerializeField] private float _colorTransitionDuration = 0.2f;
        [SerializeField] private float _colorTransitionPauseDuration = 0.5f;
        [SerializeField] private float _brightnessPulseDuration = 2f;
        [SerializeField] private float _brightnessPulseFactor = 0.2f;
        
        public float PositionTransitionSpeed => _positionTransitionSpeed;
        public float RotateTransitionSpeed => _rotateTransitionSpeed;
        public float ScaleTransitionSpeed => _scaleTransitionSpeed;
        public Ease TransformEase => _transformEase;
        public float TransformTransitionDuration => _transformTransitionDuration;
        public Color BaseColor => _baseColor;
        public Color PositionColor => _positionColor;
        public Color RotateColor => _rotateColor;
        public Color ScaleColor => _scaleColor;
        public Ease ColorEase => _colorEase;
        public float ColorTransitionDuration => _colorTransitionDuration;
        public float ColorTransitionPauseDuration => _colorTransitionPauseDuration;
        public float BrightnessPulseDuration => _brightnessPulseDuration;
        public float BrightnessPulseFactor => _brightnessPulseFactor;
    }
}