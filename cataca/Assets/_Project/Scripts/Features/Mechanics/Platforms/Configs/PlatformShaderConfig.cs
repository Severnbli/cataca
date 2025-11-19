using System;
using System.Collections.Generic;
using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts._Shared.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.Features.Mechanics.Platforms.Configs
{
    public class PlatformShaderConfig : ScriptableObjectAutoInstaller<PlatformShaderConfig>
    {
        [Header("Main")]
        
        [OnValueChanged(nameof(UpdateParameters))]
        [SerializeField] 
        private Shader _shader;

        [Header("Position")]
        
        [ShowIf(nameof(HasShader))] 
        [SerializeField]
        [ValueDropdown(nameof(GetColorProperties))]
        private string _positionColorProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        [ValueDropdown(nameof(GetFloatProperties))]
        private string _positionEnabledProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        private Color _positionColor;
        
        [Header("Rotate")]
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        [ValueDropdown(nameof(GetColorProperties))]
        private string _rotateColorProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        [ValueDropdown(nameof(GetFloatProperties))]
        private string _rotateEnabledProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        private Color _rotateColor;
        
        [Header("Scale")]

        [ShowIf(nameof(HasShader))]
        [SerializeField]
        [ValueDropdown(nameof(GetColorProperties))]
        private string _scaleColorProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        [ValueDropdown(nameof(GetFloatProperties))]
        private string _scaleEnabledProp;
        
        [ShowIf(nameof(HasShader))]
        [SerializeField]
        private Color _scaleColor;

#if UNITY_EDITOR
        private Dictionary<ShaderPropertyType, List<string>> _shaderProperties = new();

        private bool HasShader => _shader != null;

        private List<string> GetColorProperties =>
            ShaderUtils.GetPropertiesNamesByType(_shaderProperties, ShaderPropertyType.Color);
        
        private List<string> GetFloatProperties => ShaderUtils.GetPropertiesNamesByType(_shaderProperties, 
            ShaderPropertyType.Float);

        [PropertySpace(10)]
        [Button]
        private void UpdateParameters()
        {
            _shaderProperties.Clear();

            if (!HasShader)
            {
                ResetParameters();
                return;
            }
            
            _shaderProperties = ShaderUtils.GetPropertiesNamesTypes(_shader);
        }

        private void ResetParameters()
        {
            _positionColorProp = string.Empty;
            _positionEnabledProp = string.Empty;
            
            _rotateColorProp = string.Empty;
            _rotateEnabledProp = string.Empty;
            
            _scaleColorProp = string.Empty;
            _scaleEnabledProp = string.Empty;
        }

        [PropertySpace(10)]
        [Button]
        private void Validate()
        {
            var checks = new Dictionary<Func<bool>, string>();

            checks.TryAdd(() => !HasShader, "Shader not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_positionColorProp), "Position color property not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_positionEnabledProp), "Position enabled property not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_rotateColorProp), "Rotation color property not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_rotateEnabledProp), "Rotation enabled property not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_scaleColorProp), "Scale color property not set");
            checks.TryAdd(() => string.IsNullOrEmpty(_scaleEnabledProp), "Scale enabled property not set");
            
            EditorUtils.Validate(checks, nameof(PlatformShaderConfig));
        }
#endif
    }
}