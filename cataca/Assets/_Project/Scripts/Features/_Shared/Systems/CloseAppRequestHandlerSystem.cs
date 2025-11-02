using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Requests;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features._Shared.Systems
{
    public class CloseAppRequestHandlerSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<CloseAppRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.GetEntitiesCount() == 0) return;

            Application.Quit();
        }
    }
}