using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Physics.Characters.Checks.Components;
using _Project.Scripts.Features.Time.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Characters.Checks.Systems
{
    public class GroundCheckTimeBlockUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public GroundCheckTimeBlockUpdateSystem(TimeService timeService)
        {
            _timeService = timeService;
        }
        
        private TimeService _timeService;
        private EcsFilter _filter;
        private EcsPool<GroundCheckTimeBlockComponent> _groundCheckTimeBlockPool;
        private EcsPool<GroundCheckComponent> _groundCheckComponentPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GroundCheckTimeBlockComponent>()
                .Inc<GroundCheckComponent>()
                .End();
            
            _groundCheckTimeBlockPool = world.GetPool<GroundCheckTimeBlockComponent>();
            _groundCheckComponentPool = world.GetPool<GroundCheckComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var groundCheckTimeBlock = ref _groundCheckTimeBlockPool.Get(e);
                ref var groundCheck = ref _groundCheckComponentPool.Get(e);

                if (groundCheckTimeBlock.ElapsedTime < groundCheckTimeBlock.BlockTime)
                {
                    groundCheck.Disabled = true;
                    groundCheckTimeBlock.ElapsedTime +=  _timeService.DeltaTime;
                    continue;
                }
                
                groundCheck.Disabled = false;
                _groundCheckTimeBlockPool.Del(e);
            }
        }
    }
}