using System.Collections.Generic;
using _Project.Scripts._Shared.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Features.Audio.Configs
{
    public class AudioConfig : ScriptableObjectAutoInstaller<AudioConfig>
    {
        [SerializeField] private Sprite _playableIsOn;
        [SerializeField] private Sprite _playableIsOff;
        
        [SerializeField] 
#if UNITY_EDITOR
        [OnValueChanged(nameof(UpdateExposedParameters))]
#endif
        private AudioMixer _audioMixer;

        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasMixer))]
        [ValueDropdown(nameof(ExposedParameters))]
#endif
        private string _globalVolume;
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasMixer))]
        [ValueDropdown(nameof(ExposedParameters))]
#endif
        private string _musicVolume;
        
        [SerializeField]
#if UNITY_EDITOR
        [ShowIf(nameof(HasMixer))]
        [ValueDropdown(nameof(ExposedParameters))]
#endif
        private string _soundsVolume;

        public Sprite PlayableIsOn => _playableIsOn;
        public Sprite PlayableIsOff => _playableIsOff;
        public AudioMixer AudioMixer => _audioMixer;
        public string GlobalVolume => _globalVolume;
        public string MusicVolume => _musicVolume;
        public string SoundsVolume => _soundsVolume;

#if UNITY_EDITOR
        private List<string> _exposedParameters = new();
        private bool HasMixer => _audioMixer != null;
        private IEnumerable<string> ExposedParameters => _exposedParameters;
        
        [Button]
        [PropertySpace(10)]
        private void UpdateExposedParameters()
        {
            _exposedParameters.Clear();
            
            if (!HasMixer) {
                ResetParameters();
                return;
            }

            var mixerSerialized = new UnityEditor.SerializedObject(_audioMixer);
            var exposed = mixerSerialized.FindProperty("m_ExposedParameters");
            if (exposed != null && exposed.isArray)
            {
                for (int i = 0; i < exposed.arraySize; i++)
                {
                    var prop = exposed.GetArrayElementAtIndex(i);
                    var nameProp = prop.FindPropertyRelative("name");
                    if (nameProp != null)
                        _exposedParameters.Add(nameProp.stringValue);
                }
            }
        }
        
        private void ResetParameters()
        {
            _globalVolume = string.Empty;
            _musicVolume = string.Empty;
            _soundsVolume = string.Empty;
        }
#endif
    }
}