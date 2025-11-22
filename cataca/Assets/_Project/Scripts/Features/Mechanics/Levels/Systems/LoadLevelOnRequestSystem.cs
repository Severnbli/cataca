using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Monos;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Platforms.Monos;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<LoadLevelRequest> _loadLevelRequestPool;
        private EcsPool<LoadPlatformRequest> _loadPlatformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<LoadLevelRequest>()
                .End();
            
            _loadLevelRequestPool = _world.GetPool<LoadLevelRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var request = _loadLevelRequestPool.Get(e);
                LoadLevel(request.Level, e);
            }
        }

        private void LoadLevel(Level level, int entity)
        {
            var platforms = level.PlatformsParent.GetChildComponents<Platform>();

            foreach (var platform in platforms)
            {
                var platformEntity = _world.NewEntity();
                ref var loadPlatformRequest = ref _loadPlatformRequestPool.Add(platformEntity);
                loadPlatformRequest.Platform = platform;
            }
        }
    }
}