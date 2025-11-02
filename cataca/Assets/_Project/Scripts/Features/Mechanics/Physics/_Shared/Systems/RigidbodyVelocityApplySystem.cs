using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics._Shared.Systems
{
    public class RigidbodyVelocityApplySystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RigidbodyComponent>()
                .End();
            
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var rigidbody = ref _rigidbodyPool.Get(e);

                var velocity = new Vector2(
                    rigidbody.BaseXVelocity + rigidbody.AdditiveXVelocity,
                    rigidbody.Rigidbody.linearVelocity.y
                    );
                
                rigidbody.Rigidbody.linearVelocity = velocity;
                
                rigidbody.AdditiveXVelocity = 0f;
            }
        }
    }
}