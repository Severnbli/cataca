using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Markers;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Enums;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class KillPlayerOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _playersFilter;
        private EcsFilter _controlledByInputFilter;
        private EcsPool<PlayDeathAnimationRequest> _playDeathAnimationPool;
        private EcsPool<ControlledByInputMarker> _controlledByInputMarkerPool;
        private EcsPool<PlayerMarker> _playerMarkerPool;
        private EcsPool<LoadSceneOnAnimationEndMarker> _loadSceneOnAnimationEndMarkerPool;
        private EcsPool<SceneLoaderComponent> _sceneLoaderPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _playersFilter = world
                .Filter<KillPlayerRequest>()
                .Inc<PlayerMarker>()
                .End();

            _controlledByInputFilter = world
                .Filter<ControlledByInputMarker>()
                .End();
            
            _playDeathAnimationPool = world.GetPool<PlayDeathAnimationRequest>();
            _controlledByInputMarkerPool = world.GetPool<ControlledByInputMarker>();
            _playerMarkerPool = world.GetPool<PlayerMarker>();
            _sceneLoaderPool = world.GetPool<SceneLoaderComponent>();
            _loadSceneOnAnimationEndMarkerPool = world.GetPool<LoadSceneOnAnimationEndMarker>();
        }

        public void PostRun(IEcsSystems systems)
        {
            if (_playersFilter.GetEntitiesCount() == 0) return;
            
            foreach (var e in _playersFilter)
            {
                _playDeathAnimationPool.AddComponentIfNotExists(e);
                _playerMarkerPool.DelComponentIfExists(e);
                
                ref var sceneLoader = ref _sceneLoaderPool.AddComponentIfNotExists(e);
                sceneLoader.Scene = Scene.Play;
                
                _loadSceneOnAnimationEndMarkerPool.AddComponentIfNotExists(e);
            }

            foreach (var e in _controlledByInputFilter)
            {
                _controlledByInputMarkerPool.Del(e);
            }
        }
    }
}