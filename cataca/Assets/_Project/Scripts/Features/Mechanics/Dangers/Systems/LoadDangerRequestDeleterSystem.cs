using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Dangers.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Dangers.Systems
{
    public class LoadDangerRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadDangerRequest> _loadDangerRequestDeleter;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadDangerRequest>()
                .End();
            
            _loadDangerRequestDeleter = world.GetPool<LoadDangerRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadDangerRequestDeleter.Del(e);
        }
    }
}