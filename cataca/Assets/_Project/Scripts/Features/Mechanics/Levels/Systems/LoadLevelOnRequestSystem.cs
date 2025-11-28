using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Dangers.Monos;
using _Project.Scripts.Features.Mechanics.Dangers.Requests;
using _Project.Scripts.Features.Mechanics.Levels.Monos;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.Mechanics.Platforms.Monos;
using _Project.Scripts.Features.Mechanics.Platforms.Requests;
using _Project.Scripts.Features.Mechanics.Records.Monos;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<LoadLevelRequest> _loadLevelRequestPool;
        private EcsPool<LoadPlatformRequest> _loadPlatformRequestPool;
        private EcsPool<LoadRecordObjectRequest> _loadRecordObjectRequestPool;
        private EcsPool<LoadLevelCompleterRequest> _loadLevelCompleterRequestPool;
        private EcsPool<LoadDangerRequest> _loadDangerRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world
                .Filter<LoadLevelRequest>()
                .End();
            
            _loadLevelRequestPool = _world.GetPool<LoadLevelRequest>();
            _loadPlatformRequestPool = _world.GetPool<LoadPlatformRequest>();
            _loadRecordObjectRequestPool = _world.GetPool<LoadRecordObjectRequest>();
            _loadLevelCompleterRequestPool = _world.GetPool<LoadLevelCompleterRequest>();
            _loadDangerRequestPool = _world.GetPool<LoadDangerRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                var request = _loadLevelRequestPool.Get(e);
                LoadLevels(request.Level);
                LoadRecords(request.Level);
                LoadLevelCompleter(request.Level);
                LoadDangers(request.Level);
            }
        }

        private void LoadLevels(Level level)
        {
            var platforms = level.PlatformsParent.GetChildComponents<Platform>();

            foreach (var platform in platforms)
            {
                var platformEntity = _world.NewEntity();
                ref var loadPlatformRequest = ref _loadPlatformRequestPool.Add(platformEntity);
                loadPlatformRequest.Platform = platform;
            }
        }
        
        private void LoadRecords(Level level)
        {
            var records = level.RecordsParent.GetChildComponents<RecordObject>();

            foreach (var record in records)
            {
                var recordEntity = _world.NewEntity();
                ref var loadRecordObject = ref _loadRecordObjectRequestPool.Add(recordEntity);
                loadRecordObject.RecordObject = record;
            }
        }
        
        private void LoadLevelCompleter(Level level)
        {
            var levelCompleterEntity = _world.NewEntity();
            ref var loadLevelCompleterRequest =
                ref _loadLevelCompleterRequestPool.AddComponentIfNotExists(levelCompleterEntity);
            loadLevelCompleterRequest.LevelCompleter = level.LevelCompleter;
        }
        
        private void LoadDangers(Level level)
        {
            var dangers = level.DangersParent.GetChildComponents<Danger>();

            foreach (var danger in dangers)
            {
                var dangerEntity = _world.NewEntity();
                ref var loadDanger = ref _loadDangerRequestPool.Add(dangerEntity);
                loadDanger.Danger = danger;
            }
        }
    }
}