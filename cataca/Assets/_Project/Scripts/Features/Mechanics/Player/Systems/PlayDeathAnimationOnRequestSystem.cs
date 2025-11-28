using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Player.Configs;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayDeathAnimationOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public PlayDeathAnimationOnRequestSystem(PlayerAnimationConfig animationConfig)
        {
            _animationConfig = animationConfig;
        }

        private PlayerAnimationConfig _animationConfig;
        private EcsFilter _filter;
        private EcsPool<AnimatorComponent> _animatorPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayDeathAnimationRequest>()
                .End();
            
            _animatorPool = world.GetPool<AnimatorComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var animator = ref _animatorPool.Get(e);
                animator.Animator.SetTrigger(_animationConfig.DeathTrigger);
            }
        }
    }
}