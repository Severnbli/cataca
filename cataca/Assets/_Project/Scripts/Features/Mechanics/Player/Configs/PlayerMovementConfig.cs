using _Project.Scripts._Shared.ScriptableObjects;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Player.Configs
{
    public class PlayerMovementConfig : ScriptableObjectAutoInstaller<PlayerMovementConfig>
    {
        [SerializeField] private WalkComponent _walk;
        [SerializeField] private JumpComponent _jump;
        [SerializeField] private DashComponent _dash;
        [SerializeField] private float _fallThreshold = 0.5f;
        
        public WalkComponent Walk => _walk;
        public JumpComponent Jump => _jump;
        public DashComponent Dash => _dash;
        public float FallThreshold => _fallThreshold;
    }
}