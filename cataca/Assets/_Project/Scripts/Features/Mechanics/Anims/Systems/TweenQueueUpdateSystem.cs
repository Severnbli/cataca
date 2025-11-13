using System.Linq;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Anims.Systems
{
    public class TweenQueueUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<TweenComponent> _tweenPool;
        private EcsPool<TweenQueueComponent> _tweenQueuePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<TweenQueueComponent>()
                .End();
            
            _tweenPool = world.GetPool<TweenComponent>();
            _tweenQueuePool = world.GetPool<TweenQueueComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var tween = ref _tweenPool.Has(e) 
                    ? ref _tweenPool.Get(e)
                    : ref _tweenPool.Add(e);
                if (tween.Tween is { active: true }) continue;

                ref var tweenQueue = ref _tweenQueuePool.Get(e);
                tween.Tween = tweenQueue.Queue.Dequeue()?.Invoke();
                
                if (!tweenQueue.Queue.Any()) _tweenQueuePool.Del(e);
            }
        }
    }
}