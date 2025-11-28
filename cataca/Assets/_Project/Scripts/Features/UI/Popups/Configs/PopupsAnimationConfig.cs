using _Project.Scripts._Shared.ScriptableObjects;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.UI.Popups.Configs
{
    public class PopupsAnimationConfig : ScriptableObjectAutoInstaller<PopupsAnimationConfig>
    {
        [Header("Open")] 
        [SerializeField] [MinValue(0f)] private float openDuration = 1f;
        [SerializeField] [PropertyRange(0f, 1f)] private float openFadeDurationPercentage = 0.2f;
        [SerializeField] [PropertyRange(0f, 255f)] private int openFadeResultFactor = 200;
        [SerializeField] [PropertyRange(0f, 1f)] private float openBodyScale = 0f;
        [SerializeField] private Ease openEase = Ease.OutBack;
        
        [Header("Close")]
        [SerializeField] [MinValue(0f)] private float closeDuration = 0.5f;
        [SerializeField] [PropertyRange(0f, 1f)] private float closeFadeDurationPercentage = 0.5f;
        [SerializeField] [PropertyRange(0f, 255f)] private int closeFadeResultFactor = 0;
        [SerializeField] [PropertyRange(0f, 1f)] private float closeBodyScale = 0f;
        [SerializeField] private Ease closeEase = Ease.InBack;
        
        public float OpenDuration => openDuration;
        public float OpenFadeDurationPercentage => openFadeDurationPercentage;
        public int OpenFadeResultFactor => openFadeResultFactor;
        public float OpenBodyScale => openBodyScale;
        public Ease OpenEase => openEase;
        public float CloseDuration => closeDuration;
        public float CloseFadeDurationPercentage => closeFadeDurationPercentage;
        public int CloseFadeResultFactor => closeFadeResultFactor;
        public float CloseBodyScale => closeBodyScale;
        public Ease CloseEase => closeEase;
    }
}