using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class RecordPlayPauseRequestDeleter : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<RecordPlayPauseRequest> _recordPlayPauseRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RecordPlayPauseRequest>()
                .End();
            
            _recordPlayPauseRequestPool = world.GetPool<RecordPlayPauseRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _recordPlayPauseRequestPool.Del(e);
        }
    }
}