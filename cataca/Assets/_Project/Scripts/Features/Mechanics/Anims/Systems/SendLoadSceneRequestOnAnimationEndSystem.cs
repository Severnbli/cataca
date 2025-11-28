using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Anims.Components;
using _Project.Scripts.Features.Mechanics.Anims.Markers;
using _Project.Scripts.Features.Mechanics.Scenes.Components;
using _Project.Scripts.Features.Mechanics.Scenes.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Anims.Systems
{
    public class SendLoadSceneRequestOnAnimationEndSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<SceneLoaderComponent> _sceneLoaderPool;
        private EcsPool<LoadSceneRequest> _loadSceneRequestPool;
        private EcsPool<AnimatorComponent> _animatorPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadSceneOnAnimationEndMarker>()
                .Inc<SceneLoaderComponent>()
                .Inc<AnimatorComponent>()
                .End();
            
            _sceneLoaderPool = world.GetPool<SceneLoaderComponent>();
            _loadSceneRequestPool = world.GetPool<LoadSceneRequest>();
            _animatorPool = world.GetPool<AnimatorComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var animator = ref _animatorPool.Get(e);

                var stateInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.normalizedTime < 1) continue;
                
                ref var sceneLoader = ref _sceneLoaderPool.Get(e);
                ref var loadSceneRequest = ref _loadSceneRequestPool.AddComponentIfNotExists(e);
                loadSceneRequest.Scene = sceneLoader.Scene;
            }
        }
    }
}