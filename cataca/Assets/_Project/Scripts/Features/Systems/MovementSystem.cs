using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _movablesFilter;
        private EcsFilter _timeFilter;
        
        private EcsPool<VelocityComponent> _velocities;
        private EcsPool<TransformComponent> _transforms;
        private EcsPool<TimeComponent> _times;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablesFilter = world.Filter<VelocityComponent>().Inc<TransformComponent>().End();
            _timeFilter = world.Filter<TimeComponent>().End();
            
            _velocities = world.GetPool<VelocityComponent>();
            _transforms = world.GetPool<TransformComponent>();
            _times = world.GetPool<TimeComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (_timeFilter.GetEntitiesCount() == 0) return;
            
            var time = _times[_timeFilter[0]]
            
            
            foreach (var entity in _movablesFilter)
            {
                var velocity = _velocities.Get(entity);
                var transform = _transforms.Get(entity);
                
                transform.Transform.position += velocity.Value * time.DeltaTime;
            }
        }
    }
}