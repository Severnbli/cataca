using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Physics.Characters.Components;
using _Project.Scripts.Features.Physics.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Physics.Characters.Systems
{
    public class WalkDampingUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<WalkDampingComponent> _walkDampingPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<WalkDampingComponent>()
                .Inc<RigidbodyComponent>()
                .End();
            
            _walkDampingPool = world.GetPool<WalkDampingComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var walkDamping = ref _walkDampingPool.Get(e);
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                
                rigidbody.BaseXVelocity = walkDamping.Force * walkDamping.Factor;
            }
        }
    }
}