using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
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
        private EcsPool<WalkDampingComponent> _walkDampingPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<WalkComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<ControlledByInputMarker>()
                .End();
            
            _walkPool = world.GetPool<WalkComponent>();
            _walkDampingPool = world.GetPool<WalkDampingComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var walk = ref _walkPool.Get(e);
                if (!walk.Enabled) continue;
                
                var walkInput = _inputService.Walk;
                
                ref var walkDamping = ref _walkDampingPool.Has(e)
                    ? ref _walkDampingPool.Get(e)
                    : ref _walkDampingPool.Add(e);

                walkDamping.Force = walk.Force;
                walkDamping.Factor = walkInput.x;
            }
        }
    }
}