using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class SendDestroyLoadedLevelRequestOnInstantiateLevelRequestSystem : IEcsInitSystem, IEcsPostRunSystem, 
        IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<DestroyLoadedLevelRequest> _destroyLoadedLevelRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<InstantiateLevelRequest>()
                .End();
            
            _destroyLoadedLevelRequestPool = world.GetPool<DestroyLoadedLevelRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                if (!_destroyLoadedLevelRequestPool.Has(e))
                {
                    _destroyLoadedLevelRequestPool.Add(e);
                }
            }
        }
    }
}