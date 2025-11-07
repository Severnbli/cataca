using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using _Project.Scripts.Features.UI.Containers.Components;
using _Project.Scripts.Features.UI.Containers.Markers;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.UI.Containers.Systems
{
    public class LevelsContainerLoaderSystem : IEcsInitSystem, IEcsGameSystem
    {
        public LevelsContainerLoaderSystem(BuiltInStorageConfig config)
        {
            _config = config;
        }
        
        private BuiltInStorageConfig _config;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world
                .Filter<ContainerComponent>()
                .Inc<LevelsContainerMarker>()
                .End();
            
            
        }
    }
}