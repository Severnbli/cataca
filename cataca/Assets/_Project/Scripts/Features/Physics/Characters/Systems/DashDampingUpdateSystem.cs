using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Physics.Characters.Components;
using _Project.Scripts.Features.Physics.Components;
using _Project.Scripts.Features.Time.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Physics.Characters.Systems
{
    public class DashDampingUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        public DashDampingUpdateSystem(TimeService timeService)
        {
            _timeService = timeService;
        }
        
        private TimeService _timeService;
        private EcsFilter _filter;
        private EcsPool<DashDampingComponent> _dashDampingPool;
        private EcsPool<RigidbodyComponent> _rigidbodyPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<DashDampingComponent>()
                .Inc<RigidbodyComponent>()
                .End();
            
            _dashDampingPool = world.GetPool<DashDampingComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var dashDamping = ref _dashDampingPool.Get(e);

                if (dashDamping.TimePassed >= dashDamping.Duration)
                {
                    _dashDampingPool.Del(e);
                    continue;
                }
                ref var rigidbody = ref _rigidbodyPool.Get(e);
                
                var t = (dashDamping.Duration - dashDamping.TimePassed) / dashDamping.Duration;
                rigidbody.AdditiveXVelocity += dashDamping.Force * dashDamping.Factor * t;
                
                dashDamping.TimePassed += _timeService.DeltaTime;
            }
        }
    }
}