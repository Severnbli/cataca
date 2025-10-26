using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Input.Markers;
using _Project.Scripts.Features.Input.Services;
using _Project.Scripts.Features.Physics.Characters.Components;
using _Project.Scripts.Features.Physics.Components;
using _Project.Scripts.Shared.Utils;
using Leopotam.EcsLite;
using UnityEngine;

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
        private EcsPool<JumpDampingComponent> _jumpDampingPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<ControlledByInputMarker>()
                .End();
            
            _jumpPool = world.GetPool<JumpComponent>();
            _jumpDampingPool = world.GetPool<JumpDampingComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var jump = ref _jumpPool.Get(e);
                if (!jump.Enabled || jump.CurrentCount >= jump.MaxCount) continue;
                
                var jumpInput = _inputService.Jump;
                if (!jumpInput) continue;
                
                ref var jumpDamping = ref _jumpDampingPool.Has(e)
                    ? ref _jumpDampingPool.Get(e)
                    : ref _jumpDampingPool.Add(e);

                jumpDamping.Force = jump.Force;
                
                jump.CurrentCount++;
            }
        }
    }
}