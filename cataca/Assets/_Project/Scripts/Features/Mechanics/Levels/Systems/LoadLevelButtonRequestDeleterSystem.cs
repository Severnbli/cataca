using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelButtonRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadLevelButtonRequest> _loadLevelButtonRequestPool; 
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadLevelButtonRequest>()
                .End();
            
            _loadLevelButtonRequestPool = world.GetPool<LoadLevelButtonRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadLevelButtonRequestPool.Del(e);
        }
    }
}