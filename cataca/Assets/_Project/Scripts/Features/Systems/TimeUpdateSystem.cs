using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Systems
{
    public class TimeUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<TimeComponent> _timePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world.Filter<TimeComponent>().End();
            _timePool = world.GetPool<TimeComponent>();
            
            var entity = world.NewEntity();
            _timePool.Add(entity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var time = ref _timePool.Get(entity);
                time.Time = Time.time;
                time.UnscaledTime = Time.unscaledTime;
                time.DeltaTime = Time.deltaTime;
                time.UnscaledDeltaTime = Time.unscaledDeltaTime;
            }
        }
    }
}