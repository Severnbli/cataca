using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class JumpPerformRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<JumpPerformRequest> _jumpPerformRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<JumpPerformRequest>()
                .End();
            
            _jumpPerformRequestPool = world.GetPool<JumpPerformRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _jumpPerformRequestPool.Del(e);
        }
    }
}