using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Input.Markers;
using _Project.Scripts.Features.Input.Services;
using _Project.Scripts.Features.Physics.Characters.Components;
using _Project.Scripts.Features.Physics.Components;
using _Project.Scripts.Shared.Utils;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Physics.Characters.Systems
{
    public class InputBasedJumpSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public InputBasedJumpSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<JumpComponent> _jumpPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<ControlledByInputMarker>()
                .End();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var jump = ref _jumpPool.Get(e);
                if (!jump.Enabled || jump.CurrentCount >= jump.MaxCount) continue;
                
                var jumpInput = _inputService.Jump;
                if (!jumpInput) continue;
                
                var walkDirection = _inputService.Walk;
                
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                rigidbody.Rigidbody.ProcessUpperPush(jump.Force, walkDirection);
                
                jump.CurrentCount++;
            }
        }
    }
}