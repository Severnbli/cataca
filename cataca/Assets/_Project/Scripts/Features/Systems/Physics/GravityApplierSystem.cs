using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Components;
using _Project.Scripts.Features.Services;
using _Project.Scripts.Features.Systems.Physics.Components;
using _Project.Scripts.Features.Systems.Physics.Configs;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Systems.Physics
{
    public class GravityApplierSystem : IEcsRunSystem, IEcsInitSystem, IEcsGameSystem
    {
        private GravityConfig _gravityConfig;
        private TimeService _timeService;
        private EcsFilter _filter;
        private EcsPool<VelocityComponent> _velocities;
        private EcsPool<GravityComponent> _gravities;

        public GravityApplierSystem(GravityConfig gravityConfig, TimeService timeService)
        {
            _gravityConfig = gravityConfig;
            _timeService = timeService;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GravityComponent>()
                .Inc<VelocityComponent>()
                .End();
            
            _velocities = world.GetPool<VelocityComponent>();
            _gravities = world.GetPool<GravityComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            if (!_gravityConfig.isEnabled) return;
            
            foreach (var e in _filter)
            {
                ref var velocity = ref _velocities.Get(e);
                var gravity = _gravities.Get(e);
                
                var force = _gravityConfig.direction * _gravityConfig.value * gravity.Value;
                
                velocity.Value += force * _timeService.DeltaTime;
            }
        }
    }
}