using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Monos;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class LoadPlatformOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadPlatformRequest> _loadPlatformRequestPool;
        private EcsPool<PlatformComponent> _platformPool;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadPlatformRequest>()
                .End();
            
            _loadPlatformRequestPool = world.GetPool<LoadPlatformRequest>();
            _platformPool = world.GetPool<PlatformComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadPlatformRequestPool.Get(e);
                
                LoadPlatform(request.Platform, e);
            }
        }

        private void LoadPlatform(Platform platform, int entity)
        {
            ref var platformComponent = ref _platformPool.Add(entity);
            platformComponent.Platform = platform;
        }
    }
}