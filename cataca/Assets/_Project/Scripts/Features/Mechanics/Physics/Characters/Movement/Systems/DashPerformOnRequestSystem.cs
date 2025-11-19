using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class DashPerformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<DashComponent> _dashPool;
        private EcsPool<DashDampingComponent> _dashDampingPool;
        private EcsPool<DashPerformRequest> _dashPerformRequestPool;
        private EcsPool<PlayDashAnimationRequest> _playDashAnimationRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<DashPerformRequest>()
                .Inc<DashComponent>()
                .Inc<RigidbodyComponent>()
                .Inc<WalkDampingComponent>()
                .End();
            
            _dashPool = world.GetPool<DashComponent>();
            _dashDampingPool = world.GetPool<DashDampingComponent>();
            _dashPerformRequestPool = world.GetPool<DashPerformRequest>();
            _playDashAnimationRequestPool = world.GetPool<PlayDashAnimationRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var dash = ref _dashPool.Get(e);
                if (!dash.Enabled || dash.CurrentCount >= dash.MaxCount) continue;
                
                ref var dashPerformRequest = ref _dashPerformRequestPool.Get(e);
                if (Mathf.Approximately(dashPerformRequest.Factor, 0f)) continue;
                dash.CurrentCount++;
                
                if (!_playDashAnimationRequestPool.Has(e)) _playDashAnimationRequestPool.Add(e);
                
                ref var dashDamping = ref _dashDampingPool.Has(e)
                    ? ref _dashDampingPool.Get(e)
                    : ref _dashDampingPool.Add(e);
                
                dashDamping.Force = dash.Force;
                dashDamping.Factor = dashPerformRequest.Factor;
                dashDamping.Duration = dash.Duration;
                dashDamping.TimePassed = 0f;
            }
        }
    }
}