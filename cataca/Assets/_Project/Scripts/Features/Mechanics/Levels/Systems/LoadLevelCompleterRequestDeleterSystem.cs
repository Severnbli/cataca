using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelCompleterRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadLevelCompleterRequest> _loadLevelCompleterRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadLevelCompleterRequest>()
                .End();
            
            _loadLevelCompleterRequestPool = world.GetPool<LoadLevelCompleterRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadLevelCompleterRequestPool.Del(e);
        }
    }
}