using _Project.Scripts.Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Features.Input.Configs
{
    public class InputControlsConfig : ScriptableObjectAutoInstaller<InputControlsConfig>
    {
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference jump;
        [SerializeField] private InputActionReference dash;
        [SerializeField] private InputActionReference position;
        [SerializeField] private InputActionReference rotation;
        [SerializeField] private InputActionReference scale;
        
        public InputActionReference Move => move;
        public InputActionReference Jump => jump;
        public InputActionReference Dash => dash;
        public InputActionReference Position => position;
        public InputActionReference Rotation => rotation;
        public InputActionReference Scale => scale;
    }
}