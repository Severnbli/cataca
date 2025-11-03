using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Player.Configs;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayerWalkAnimationUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        public PlayerWalkAnimationUpdateSystem(PlayerAnimationConfig config)
        {
            _config = config;
        }

        private PlayerAnimationConfig _config;
        private EcsFilter _filter;
        private EcsPool<AnimatorComponent> _animatorPool;
        private EcsPool<WalkDampingComponent> _walkDampingPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayerMarker>()
                .Inc<AnimatorComponent>()
                .Inc<WalkDampingComponent>()
                .End();
            
            _animatorPool = world.GetPool<AnimatorComponent>();
            _walkDampingPool = world.GetPool<WalkDampingComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var animator = ref _animatorPool.Get(e);
                ref var walkDamping = ref _walkDampingPool.Get(e);

                var isWalking = !Mathf.Approximately(walkDamping.Factor, 0f);
                
                animator.Animator.SetBool(_config.IsWalking, isWalking);
            }
        }
    }
}