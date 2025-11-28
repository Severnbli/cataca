using _Project.Scripts._Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Features.Mechanics.Input.Configs
{
    public class InputControlsConfig : ScriptableObjectAutoInstaller<InputControlsConfig>
    {
        [SerializeField] private InputActionReference _walk;
        [SerializeField] private InputActionReference _jump;
        [SerializeField] private InputActionReference _dash;
        [SerializeField] private InputActionReference _position;
        [SerializeField] private InputActionReference _rotation;
        [SerializeField] private InputActionReference _scale;
        
        public InputActionReference Walk => _walk;
        public InputActionReference Jump => _jump;
        public InputActionReference Dash => _dash;
        public InputActionReference Position => _position;
        public InputActionReference Rotation => _rotation;
        public InputActionReference Scale => _scale;

        protected override void RegisterBindings()
        {
            _walk.action.Enable();
            _jump.action.Enable();
            _dash.action.Enable();
            _position.action.Enable();
            _rotation.action.Enable();
            _scale.action.Enable();
        }
    }
}