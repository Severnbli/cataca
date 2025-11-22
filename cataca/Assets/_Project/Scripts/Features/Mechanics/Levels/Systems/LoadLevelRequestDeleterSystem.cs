using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadLevelRequest> _loadLevelRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadLevelRequest>()
                .End();
            
            _loadLevelRequestPool = world.GetPool<LoadLevelRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadLevelRequestPool.Del(e);
        }
    }
}