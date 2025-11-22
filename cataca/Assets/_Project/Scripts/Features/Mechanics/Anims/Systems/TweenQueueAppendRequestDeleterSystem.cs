using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Anims.Systems
{
    public class TweenQueueAppendRequestDeleterSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<TweenQueueAppendRequest> _tweenQueueAppendRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<TweenQueueAppendRequest>()
                .End();
            
            _tweenQueueAppendRequestPool = world.GetPool<TweenQueueAppendRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter) _tweenQueueAppendRequestPool.Del(e);
        }
    }
}