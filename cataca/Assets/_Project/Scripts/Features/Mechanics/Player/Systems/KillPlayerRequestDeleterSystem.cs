using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class KillPlayerRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<KillPlayerRequest> _killPlayerRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<KillPlayerRequest>()
                .End();
            
            _killPlayerRequestPool = world.GetPool<KillPlayerRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _killPlayerRequestPool.Del(e);
        }
    }
}