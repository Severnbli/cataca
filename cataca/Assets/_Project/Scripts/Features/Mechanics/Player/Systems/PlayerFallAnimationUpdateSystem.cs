using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Components;
using _Project.Scripts.Features.Mechanics.Player.Configs;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayerFallAnimationUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PlayerFallAnimationUpdateSystem(PlayerAnimationConfig config)
        {
            _config = config;
        }
        
        private PlayerAnimationConfig _config;
        private EcsFilter _filter;
        private EcsPool<AnimatorComponent> _animatorPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        private EcsPool<GroundCheckStatusComponent> _groundCheckStatusPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<AnimatorComponent>()
                .Inc<PlayerMarker>()
                .End();
            
            _animatorPool = world.GetPool<AnimatorComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
            _groundCheckStatusPool = world.GetPool<GroundCheckStatusComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var isFalling = GetIsFalling(e);
                ref var animator = ref _animatorPool.Get(e);
                animator.Animator.SetBool(_config.IsFalling, isFalling);
            }
        }

        private bool GetIsFalling(int entity)
        {
            if (_groundCheckStatusPool.Has(entity))
            {
                ref var groundCheckStatus = ref _groundCheckStatusPool.Get(entity);
                return !groundCheckStatus.IsOnGround;
            }
            
            if (_rigidbodyPool.Has(entity))
            {
                ref var rigidbody = ref _rigidbodyPool.Get(entity);
                return rigidbody.Rigidbody.linearVelocity.y < 0;
            }
            
            return false;
        }
    }
}