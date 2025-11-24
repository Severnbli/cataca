using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Systems
{
    public class GroundCheckStatusUpdateSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<GroundCheckComponent> _groundCheckPool;
        private EcsPool<GroundCheckStatusComponent> _groundCheckStatusPool;
        private EcsPool<GroundCheckResultComponent> _groundCheckResultPool;
        private EcsPool<TransformComponent> _transformPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GroundCheckComponent>()
                .End();
            
            _groundCheckPool = world.GetPool<GroundCheckComponent>();
            _groundCheckStatusPool = world.GetPool<GroundCheckStatusComponent>();
            _groundCheckResultPool = world.GetPool<GroundCheckResultComponent>();
            _transformPool = world.GetPool<TransformComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var groundCheck = ref _groundCheckPool.Get(e);
                if (groundCheck.Transform is null) continue;
                
                ref var groundCheckStatus = ref _groundCheckStatusPool.Has(e)
                    ? ref _groundCheckStatusPool.Get(e)
                    : ref _groundCheckStatusPool.Add(e);

                groundCheckStatus.IsOnGround = Physics2DExtensions.TryHitCollider(groundCheck.Transform.position,
                    Vector2.down, groundCheck.Distance, groundCheck.Layer, out var hit) && !groundCheck.Disabled;
                
                if (!groundCheckStatus.IsOnGround || !groundCheck.ResultSaving || !_transformPool.Has(e))
                {
                    _groundCheckResultPool.DelComponentIfExists(e);
                    continue;
                }
                
                ref var characterTransform = ref _transformPool.Get(e);
                ref var groundCheckResult = ref _groundCheckResultPool.AddComponentIfNotExists(e);
                
                groundCheckResult.Transform = hit.collider.transform;
                groundCheckResult.Offset = groundCheckResult.Transform.localPosition -
                                           characterTransform.Transform.localPosition;
            }
        }
    }
}