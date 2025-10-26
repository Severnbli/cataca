using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Physics.Characters.Checks.Components;
using _Project.Scripts.Features.Physics.Characters.Movement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Characters.Checks.Systems
{
    public class DashResetCurrentCountOnGroundCheckSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<GroundCheckStatusComponent> _groundCheckStatusPool;
        private EcsPool<DashComponent> _dashPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GroundCheckStatusComponent>()
                .Inc<DashComponent>()
                .End();
            
            _groundCheckStatusPool = world.GetPool<GroundCheckStatusComponent>();
            _dashPool = world.GetPool<DashComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var groundCheckStatus = ref _groundCheckStatusPool.Get(e);
                if (!groundCheckStatus.IsOnGround) continue;
                
                ref var dashComponent = ref _dashPool.Get(e);
                dashComponent.CurrentCount = 0;
            }
        }
    }
}