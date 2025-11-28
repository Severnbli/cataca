using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features._Shared.Components;
using _Project.Scripts.Features.UI._Shared.Markers;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.UI._Shared.Systems
{
    public class EnableTouchOnlyInputOnNonDesktopSystem : IEcsInitSystem, IEcsGameSystem
    {
        public void Init(IEcsSystems systems)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop) return;
            
            var world = systems.GetWorld();

            var filter = world
                .Filter<TouchOnlyInputMarker>()
                .Inc<TransformComponent>()
                .End();
            
            var transformPool = world.GetPool<TransformComponent>();

            foreach (var e in filter)
            {
                ref var transform = ref transformPool.Get(e);
                transform.Transform.gameObject.SetActive(true);
            }
        }
    }
}