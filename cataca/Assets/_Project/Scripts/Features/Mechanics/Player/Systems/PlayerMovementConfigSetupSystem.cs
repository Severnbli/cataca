using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Player.Configs;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayerMovementConfigSetupSystem : IEcsInitSystem, IEcsGameSystem
    {
        public PlayerMovementConfigSetupSystem(PlayerMovementConfig playerMovementConfig)
        {
            _playerMovementConfig = playerMovementConfig;
        }
        
        private PlayerMovementConfig _playerMovementConfig;
        private EcsFilter _filter;
        private EcsPool<WalkComponent> _walkPool;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<DashComponent> _dashPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayerMarker>()
                .Inc<RigidbodyComponent>()
                .End();
            
            _walkPool = world.GetPool<WalkComponent>();
            _jumpPool = world.GetPool<JumpComponent>();
            _dashPool = world.GetPool<DashComponent>();
            
            SetupPlayers();
        }

        private void SetupPlayers()
        {
            foreach (var e in _filter)
            {
                if (!_walkPool.Has(e))
                {
                    ref var walk = ref _walkPool.Add(e);
                    walk = _playerMovementConfig.Walk;
                }

                if (!_jumpPool.Has(e))
                {
                    ref var jump = ref _jumpPool.Add(e);
                    jump = _playerMovementConfig.Jump;
                }

                if (!_dashPool.Has(e))
                {
                    ref var dash = ref _dashPool.Add(e);
                    dash = _playerMovementConfig.Dash;
                }
            }
        }
    }
}