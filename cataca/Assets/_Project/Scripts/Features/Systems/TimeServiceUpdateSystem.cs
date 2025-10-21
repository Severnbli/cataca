using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Components;
using _Project.Scripts.Features.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Systems
{
    public class TimeServiceUpdateSystem : IEcsRunSystem, IEcsGameSystem
    {
        private readonly TimeService _timeService;

        public TimeServiceUpdateSystem(TimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Run(IEcsSystems systems)
        {
            _timeService.Time = Time.time;
            _timeService.UnscaledTime = Time.unscaledTime;
            _timeService.DeltaTime = Time.deltaTime;
            _timeService.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
    }
}