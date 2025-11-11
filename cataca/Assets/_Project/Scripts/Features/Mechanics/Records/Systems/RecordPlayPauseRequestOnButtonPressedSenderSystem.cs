using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Records.Components;
using _Project.Scripts.Features.Mechanics.Records.Requests;
using _Project.Scripts.Features.UI.Buttons.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Records.Systems
{
    public class RecordPlayPauseRequestOnButtonPressedSenderSystem : IEcsInitSystem, IEcsRunSystem, IEcsGameSystem
    {
        private EcsFilter _filter;
        private EcsPool<ButtonComponent> _buttonPool;
        private EcsPool<RecordPlayPauseRequest> _recordPlayPauseRequestPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _filter = world
                .Filter<RecordComponent>()
                .Inc<ButtonComponent>()
                .End();
            
            _buttonPool = world.GetPool<ButtonComponent>();
            _recordPlayPauseRequestPool = world.GetPool<RecordPlayPauseRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var e in _filter)
            {
                ref var button = ref _buttonPool.Get(e);
                
                if (!button.Button.Pressed) continue;
                if (_recordPlayPauseRequestPool.Has(e)) continue;
                
                _recordPlayPauseRequestPool.Add(e);
            }
        }
    }
}