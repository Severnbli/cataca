using _Project.Scripts._Shared.Extensions;
using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Markers;
using _Project.Scripts.Features.Mechanics.Player.Markers;
using _Project.Scripts.Features.Mechanics.Player.Requests;
using Leopotam.EcsLite;

namespace _Project.Scripts.Features.Mechanics.Player.Systems
{
    public class KillPlayerOnRequestSystem : IEcsInitSystem, IEcsPostRunSystem, IEcsGameSystem
    {
        private EcsFilter _playersFilter;
        private EcsFilter _controlledByInputFilter;
        private EcsPool<PlayDeathAnimationRequest> _playDeathAnimationPool;
        private EcsPool<ControlledByInputMarker> _controlledByInputMarkerPool;
        
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
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (var e in _playersFilter)
            {
                _playDeathAnimationPool.AddComponentIfNotExists(e);
            }

            foreach (var e in _controlledByInputFilter)
            {
                _controlledByInputMarkerPool.Del(e);
            }
        }
    }
}