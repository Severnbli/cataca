using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LevelButtonUpdateSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LevelButtonComponent> _levelButtonPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LevelButtonComponent>()
                .End();
            
            _levelButtonPool = world.GetPool<LevelButtonComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var levelButtonComponent = ref _levelButtonPool.Get(e);
                var levelButton = levelButtonComponent.LevelButton;
                
                if (levelButton.LevelDto is null) continue;
                
                levelButton.Name.text = levelButton.LevelDto.Name;
            }
        }
    }
}