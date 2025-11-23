using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class ResetPlatformStatesRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ResetPlatformStatesRequest> _resetPlatformStatesRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<ResetPlatformStatesRequest>()
                .End();
            
            _resetPlatformStatesRequestPool = world.GetPool<ResetPlatformStatesRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _resetPlatformStatesRequestPool.Del(e);
        }
    }
}