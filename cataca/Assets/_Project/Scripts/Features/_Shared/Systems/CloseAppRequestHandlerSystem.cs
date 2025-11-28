using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Requests;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features._Shared.Systems
{
    public class CloseAppRequestHandlerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<CloseAppRequest> _closeAppRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<CloseAppRequest>()
                .End();
            
            _closeAppRequestPool = world.GetPool<CloseAppRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.GetEntitiesCount() == 0) return;
            
            foreach (var e in _filter) _closeAppRequestPool.Del(e);

            Application.Quit();
        }
    }
}