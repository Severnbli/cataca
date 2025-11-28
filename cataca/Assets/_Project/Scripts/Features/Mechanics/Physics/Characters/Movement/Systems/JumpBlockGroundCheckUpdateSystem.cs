using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class JumpBlockGroundCheckUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<JumpBlockGroundCheckComponent> _jumpBlockGroundCheckPool;
        private EcsPool<GroundCheckTimeBlockComponent> _groundCheckTimeBlockPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpBlockGroundCheckComponent>()
                .Inc<JumpPerformRequest>()
                .End();
            
            _jumpBlockGroundCheckPool = world.GetPool<JumpBlockGroundCheckComponent>();
            _groundCheckTimeBlockPool = world.GetPool<GroundCheckTimeBlockComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var jumpBlockGroundCheck = ref _jumpBlockGroundCheckPool.Get(e);
                
                ref var groundCheckTimeBlock = ref _groundCheckTimeBlockPool.Has(e)
                    ? ref _groundCheckTimeBlockPool.Get(e)
                    : ref _groundCheckTimeBlockPool.Add(e);

                if (jumpBlockGroundCheck.BlockTime <= groundCheckTimeBlock.BlockTime - groundCheckTimeBlock.ElapsedTime)
                {
                    continue;
                }
                
                groundCheckTimeBlock.BlockTime = jumpBlockGroundCheck.BlockTime;
                groundCheckTimeBlock.ElapsedTime = 0f;
            }
        }
    }
}