using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Systems
{
    public class JumpResetCurrentCountOnGroundCheckSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<GroundCheckStatusComponent> _groundCheckStatusPool;
        private EcsPool<JumpComponent> _jumpPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GroundCheckStatusComponent>()
                .Inc<JumpComponent>()
                .End();
            
            _groundCheckStatusPool = world.GetPool<GroundCheckStatusComponent>();
            _jumpPool = world.GetPool<JumpComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var groundCheckStatus = ref _groundCheckStatusPool.Get(e);
                if (!groundCheckStatus.IsOnGround) continue;
                
                ref var jumpComponent = ref _jumpPool.Get(e);
                jumpComponent.CurrentCount = 0;
            }
        }
    }
}