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
    public class InputBasedWalkSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public InputBasedWalkSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<WalkComponent> _walkPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<WalkComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<ControlledByInputMarker>()
                .End();
            
            _walkPool = world.GetPool<WalkComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var walk = ref _walkPool.Get(e);
                if (!walk.Enabled) continue;
                
                var walkInput = _inputService.Walk;
                
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                rigidbody.Rigidbody.ProcessWalk(walk.Force, walkInput);
            }
        }
    }
}