using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class FlipObjectOnWalkSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<FlipObjectOnWalkComponent> _flipObjectOnWalkPool;
        private EcsPool<WalkDampingComponent> _walkDampingPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<FlipObjectOnWalkComponent>()
                .Inc<WalkDampingComponent>()
                .End();
            
            _flipObjectOnWalkPool = world.GetPool<FlipObjectOnWalkComponent>();
            _walkDampingPool = world.GetPool<WalkDampingComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var flipObjectOnWalk = ref _flipObjectOnWalkPool.Get(e);
                ref var walkDamping = ref _walkDampingPool.Get(e);

                if (Mathf.Approximately(walkDamping.Factor, 0f)) continue;
                
                var originalScale = flipObjectOnWalk.Object.transform.localScale;
                originalScale.x = Mathf.Sign(walkDamping.Factor) * Mathf.Abs(originalScale.x);
                flipObjectOnWalk.Object.transform.localScale = originalScale;
            }
        }
    }
}