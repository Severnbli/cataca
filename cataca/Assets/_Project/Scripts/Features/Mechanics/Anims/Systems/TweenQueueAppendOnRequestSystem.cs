using System;
using System.Collections.Generic;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Anims.Requests;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Anims.Systems
{
    public class TweenQueueAppendOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<TweenQueueAppendRequest> _tweenQueueAppendRequestPool;
        private EcsPool<TweenQueueComponent> _tweenQueuePool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<TweenQueueAppendRequest>()
                .End();
            
            _tweenQueueAppendRequestPool = world.GetPool<TweenQueueAppendRequest>();
            _tweenQueuePool = world.GetPool<TweenQueueComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var tweenQueue = ref _tweenQueuePool.Has(e)
                    ? ref _tweenQueuePool.Get(e)
                    : ref _tweenQueuePool.Add(e);

                ref var tweenQueueAppendRequest = ref _tweenQueueAppendRequestPool.Get(e);
                
                tweenQueue.Queue ??= new Queue<Func<Tween>>();
                tweenQueue.Queue.Enqueue(tweenQueueAppendRequest.Func);
            }
        }
    }
}