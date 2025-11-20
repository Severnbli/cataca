using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Platforms.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Platforms.Systems
{
    public class PlatformGlowAddSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<PlatformGlowComponent> _platformGlowPool;
        private EcsPool<TweenComponent> _tweenPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<PlatformComponent>()
                .Exc<PlatformGlowComponent>()
                .End();
            
            _platformGlowPool = _world.GetPool<PlatformGlowComponent>();
            _tweenPool = _world.GetPool<TweenComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var platformGlow = ref _platformGlowPool.Add(e);
                
                platformGlow.PulseAnimEntity = _world.NewEntity();
                _tweenPool.Add(platformGlow.PulseAnimEntity);
                
                platformGlow.TransitionAnimEntity = _world.NewEntity();
                _tweenPool.Add(platformGlow.TransitionAnimEntity);
            }
        }
    }
}