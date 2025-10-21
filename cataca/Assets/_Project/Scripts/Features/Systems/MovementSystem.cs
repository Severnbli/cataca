using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Components;
using _Project.Scripts.Features.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _movablesFilter;
        private EcsPool<VelocityComponent> _velocities;
        private EcsPool<TransformComponent> _transforms;
        private readonly TimeService _timeService;

        public MovementSystem(TimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablesFilter = world.Filter<VelocityComponent>().Inc<TransformComponent>().End();
            
            _velocities = world.GetPool<VelocityComponent>();
            _transforms = world.GetPool<TransformComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _movablesFilter)
            {
                var velocity = _velocities.Get(entity);
                var transform = _transforms.Get(entity);
                
                transform.Transform.position += velocity.Value * _timeService.DeltaTime;
            }
        }
    }
}