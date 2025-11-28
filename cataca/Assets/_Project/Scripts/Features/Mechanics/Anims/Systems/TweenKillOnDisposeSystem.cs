using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using DG.Tweening;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Anims.Systems
{
    public class TweenKillOnDisposeSystem : IEcsInitSystem, IEcsDestroySystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<TweenComponent>()
                .End();
            
            _tweenPool = world.GetPool<TweenComponent>();
        }

        public void Destroy(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var tween = ref _tweenPool.Get(e);
                tween.Tween?.Kill();
            }
        }
    }
}