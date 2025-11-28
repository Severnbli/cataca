using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class PlayJumpAnimationRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlayJumpAnimationRequest> _playJumpAnimationRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<PlayJumpAnimationRequest>()
                .End();
            
            _playJumpAnimationRequestPool = world.GetPool<PlayJumpAnimationRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _playJumpAnimationRequestPool.Del(e);
        }
    }
}