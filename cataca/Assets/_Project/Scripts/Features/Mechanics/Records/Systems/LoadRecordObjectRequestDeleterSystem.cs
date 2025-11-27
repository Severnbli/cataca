using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class LoadRecordObjectRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadRecordObjectRequest> _loadRecordObjectRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadRecordObjectRequest>()
                .End();
            
            _loadRecordObjectRequestPool = world.GetPool<LoadRecordObjectRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadRecordObjectRequestPool.Del(e);
        }
    }
}