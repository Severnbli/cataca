using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Components;
using _Project.Scripts.Shared.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class JumpDampingUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<JumpDampingComponent> _jumpDampingPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpDampingComponent>()
                .Inc<RigidbodyComponent>()
                .End();
            
            _jumpDampingPool = world.GetPool<JumpDampingComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var jumpDamping = ref _jumpDampingPool.Get(e);
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                
                rigidbody.Rigidbody.AddVerticalForce(Mathf.Abs(jumpDamping.Force));
                
                _jumpDampingPool.Del(e);
            }
        }
    }
}