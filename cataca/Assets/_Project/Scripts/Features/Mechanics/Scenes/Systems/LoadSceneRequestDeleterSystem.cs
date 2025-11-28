using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Scenes.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Scenes.Systems
{
    public class LoadSceneRequestDeleterSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadSceneRequest> _loadSceneRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadSceneRequest>()
                .End();
            
            _loadSceneRequestPool = world.GetPool<LoadSceneRequest>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter) _loadSceneRequestPool.Del(e);
        }
    }
}