using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Time.Services;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Time.Systems
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
            _timeService.Time = UnityEngine.Time.time;
            _timeService.UnscaledTime = UnityEngine.Time.unscaledTime;
            _timeService.DeltaTime = UnityEngine.Time.deltaTime;
            _timeService.UnscaledDeltaTime = UnityEngine.Time.unscaledDeltaTime;
        }
    }
}