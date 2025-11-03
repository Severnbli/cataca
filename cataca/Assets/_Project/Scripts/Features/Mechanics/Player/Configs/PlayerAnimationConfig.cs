using System;
using System.Collections.Generic;
using _Project.Scripts._Shared.Extensions;
using _Project.Scripts._Shared.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Player.Configs
{
    public class PlayerAnimationConfig : ScriptableObjectAutoInstaller<PlayerAnimationConfig>
    {
        [SerializeField] 
        [OnValueChanged(nameof(UpdateParameters))]
        private AnimatorController _animatorController;

        [SerializeField]
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(BoolParameters))]
        private string _isWalking;
        
        [SerializeField]
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(BoolParameters))]
        private string _isFalling;

        [SerializeField]
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
        private string _jumpTrigger;
        
        [SerializeField]
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
        private string _dashTrigger;
        
        [SerializeField]
        [ShowIf(nameof(HasAnimator))]
        [ValueDropdown(nameof(TriggerParameters))]
        private string _deathTrigger;
        
        public string IsWalking => _isWalking;
        public string IsFalling => _isFalling;
        public string JumpTrigger => _jumpTrigger;
        public string DashTrigger => _dashTrigger;
        public string DeathTrigger => _deathTrigger;

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
    }
}