using System;
using System.Collections.Generic;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts._Shared.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace _Project.Scripts.Features.Mechanics.Player.Configs
{
    public class PlayerAnimationConfig : ScriptableObjectAutoInstaller<PlayerAnimationConfig>
    {
#if UNITY_EDITOR
        [SerializeField] 
        [OnValueChanged(nameof(UpdateParameters))]
        private AnimatorController _animatorController;
#endif
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(BoolParameters))]
#endif
        private string _isWalking;
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(BoolParameters))]
#endif
        private string _isFalling;

        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
#endif
        private string _jumpTrigger;
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
#endif
        private string _dashTrigger;
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
#endif
        private string _deathTrigger;
        
        public string IsWalking => _isWalking;
        public string IsFalling => _isFalling;
        public string JumpTrigger => _jumpTrigger;
        public string DashTrigger => _dashTrigger;
        public string DeathTrigger => _deathTrigger;
        
#if UNITY_EDITOR
        private Dictionary<Type, List<string>> _parameters = new();
        
        private bool HasAnimator => _animatorController != null;
        private IEnumerable<string> BoolParameters => _parameters.GetParametersWithType<bool>();
        private IEnumerable<string> TriggerParameters => _parameters.GetParametersWithType<AnimatorExtensions.Trigger>();

        private void UpdateParameters()
        {
            if (!HasAnimator)
            {
                ResetParameters();
            }
            
            _animatorController.TryGetParametersWithTypes(out var parameters);
            _parameters = parameters;
        }

        private void ResetParameters()
        {
            _isWalking = string.Empty;
            _isFalling = string.Empty;
            _jumpTrigger = string.Empty;
            _dashTrigger = string.Empty;
            _deathTrigger = string.Empty;
        }
#endif
    }
}