using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayDashAnimationRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlayDashAnimationRequest> _playDashAnimationRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayDashAnimationRequest>()
                .End();
            
            _playDashAnimationRequestPool = world.GetPool<PlayDashAnimationRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _playDashAnimationRequestPool.Del(e);
        }
    }
}