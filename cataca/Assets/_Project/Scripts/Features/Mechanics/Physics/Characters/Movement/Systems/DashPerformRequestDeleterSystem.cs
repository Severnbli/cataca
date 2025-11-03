using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class DashPerformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<DashPerformRequest> _dashPerformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<DashPerformRequest>()
                .End();
            
            _dashPerformRequestPool = world.GetPool<DashPerformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _dashPerformRequestPool.Del(e);
        }
    }
}