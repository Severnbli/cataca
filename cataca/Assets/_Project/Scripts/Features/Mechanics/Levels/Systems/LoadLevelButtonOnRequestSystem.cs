using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Levels.Components;
using _Project.Scripts.Features.Mechanics.Levels.Requests;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Levels.Systems
{
    public class LoadLevelButtonOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadLevelButtonRequest> _loadLevelButtonRequestPool;
        private EcsPool<LevelButtonComponent> _levelButtonPool;
        private EcsPool<ButtonComponent> _buttonPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<LoadLevelButtonRequest>()
                .End();
            
            _loadLevelButtonRequestPool = world.GetPool<LoadLevelButtonRequest>();
            _levelButtonPool = world.GetPool<LevelButtonComponent>();
            _buttonPool = world.GetPool<ButtonComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var request = ref _loadLevelButtonRequestPool.Get(e);
                ref var levelButton = ref _levelButtonPool.AddComponentIfNotExists(e);
                ref var button = ref _buttonPool.AddComponentIfNotExists(e);

                levelButton.LevelButton = request.LevelButton;
                button.Button = request.LevelButton.Button;
            }
        }
    }
}