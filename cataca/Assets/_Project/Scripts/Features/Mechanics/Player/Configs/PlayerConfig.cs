using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Shared.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Player.Configs
{
    public class PlayerConfig : ScriptableObjectAutoInstaller<PlayerConfig>
    {
        [SerializeField] private WalkComponent _walk;
        [SerializeField] private JumpComponent _jump;
        [SerializeField] private DashComponent _dash;
        
        public WalkComponent Walk => _walk;
        public JumpComponent Jump => _jump;
        public DashComponent Dash => _dash;
    }
}