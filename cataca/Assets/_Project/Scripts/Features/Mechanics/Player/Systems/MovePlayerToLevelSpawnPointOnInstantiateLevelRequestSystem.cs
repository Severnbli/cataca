using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class MovePlayerToLevelSpawnPointOnInstantiateLevelRequestSystem : IEcsInitSystem, IEcsPostRunSystem, 
        IEcsGameSystem
    {
        private EcsFilter _playerFilter;
        private EcsFilter _instantiateLevelRequestFilter;
        private EcsPool<MoveToLevelSpawnPointRequest> _moveToLevelSpawnPointRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _playerFilter = world
                .Filter<PlayerMarker>()
                .End();

            _instantiateLevelRequestFilter = world
                .Filter<InstantiateLevelRequest>()
                .End();
            
            _moveToLevelSpawnPointRequestPool = world.GetPool<MoveToLevelSpawnPointRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_instantiateLevelRequestFilter.GetEntitiesCount() == 0) return;

            foreach (var e in _playerFilter)
            {
                _moveToLevelSpawnPointRequestPool.AddComponentIfNotExists(e);
            }
        }
    }
}