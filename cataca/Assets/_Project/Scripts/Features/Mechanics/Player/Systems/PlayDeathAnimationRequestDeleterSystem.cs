using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayDeathAnimationRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlayDeathAnimationRequest> _playDeathAnimationRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayDeathAnimationRequest>()
                .End();
            
            _playDeathAnimationRequestPool = world.GetPool<PlayDeathAnimationRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _playDeathAnimationRequestPool.Del(e);
        }
    }
}