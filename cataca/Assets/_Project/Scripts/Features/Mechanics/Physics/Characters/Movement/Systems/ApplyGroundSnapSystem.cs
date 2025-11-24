using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.Mechanics.Physics.Characters.Checks.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Physics.Characters.Movement.Systems
{
    public class ApplyGroundSnapSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<GroundCheckResultComponent> _groundCheckResultPool;
        private EcsPool<TransformComponent> _transformPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<GroundCheckResultComponent>()
                .Inc<TransformComponent>()
                .End();
            
            _groundCheckResultPool = world.GetPool<GroundCheckResultComponent>();
            _transformPool = world.GetPool<TransformComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var groundCheckResult = ref _groundCheckResultPool.Get(e);
                ref var transform = ref _transformPool.Get(e);
                
                transform.Transform.position = groundCheckResult.Transform.position - groundCheckResult.Offset;
            }
        }
    }
}