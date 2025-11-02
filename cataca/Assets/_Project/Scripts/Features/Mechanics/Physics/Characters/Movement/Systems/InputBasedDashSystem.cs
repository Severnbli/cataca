using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Input.Services;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class InputBasedDashSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public InputBasedDashSystem(InputService inputService)
        {
            _inputService = inputService;
        }
        
        private InputService _inputService;
        private EcsFilter _filter;
        private EcsPool<DashComponent> _dashPool;
        private EcsPool<DashDampingComponent> _dashDampingPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<DashComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<ControlledByInputMarker>()
                .End();
            
            _dashPool = world.GetPool<DashComponent>();
            _dashDampingPool = world.GetPool<DashDampingComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var dash = ref _dashPool.Get(e);
                if (!dash.Enabled || dash.CurrentCount >= dash.MaxCount) continue;

                var dashInput = _inputService.Dash;
                if (!dashInput) continue;

                var walkInput = _inputService.Walk.x;
                
                ref var dashDamping = ref _dashDampingPool.Has(e)
                    ? ref _dashDampingPool.Get(e)
                    : ref _dashDampingPool.Add(e);

                dashDamping.Force = dash.Force;
                dashDamping.Factor = walkInput;
                dashDamping.Duration = dash.Duration;
                dashDamping.TimePassed = 0f;
                
                dash.CurrentCount++;
            }
        }
    }
}