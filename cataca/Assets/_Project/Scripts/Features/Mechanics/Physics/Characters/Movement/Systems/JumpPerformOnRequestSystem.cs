using _Project.Scripts._Shared.Utils;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class JumpPerformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        private EcsPool<PlayJumpAnimationRequest> _playJumpAnimationRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpPerformRequest>()
                .Inc<JumpComponent>()
                .Inc<RigidbodyComponent>()
                .End();
            
            _jumpPool = world.GetPool<JumpComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
            _playJumpAnimationRequestPool = world.GetPool<PlayJumpAnimationRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var jump = ref _jumpPool.Get(e);
                if (!jump.Enabled || jump.CurrentCount >= jump.MaxCount) continue;
                jump.CurrentCount++;
                
                if (!_playJumpAnimationRequestPool.Has(e)) _playJumpAnimationRequestPool.Add(e);
                
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                rigidbody.Rigidbody.AddVerticalForce(Mathf.Abs(jump.Force));
            }
        }
    }
}