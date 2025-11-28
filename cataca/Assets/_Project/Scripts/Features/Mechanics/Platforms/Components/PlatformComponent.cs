using System;
using _Project.Scripts.Features.Mechanics.Platforms.Monos;

namespace _Project.Scripts.Features.Mechanics.Platforms.Components
{
    [Serializable]
    public struct PlatformComponent
    {
        public Platform Platform;

        [NonSerialized] public int PositionId;
        [NonSerialized] public int RotateId;
        [NonSerialized] public int ScaleId;
    }
}