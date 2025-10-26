using System;

namespace _Project.Scripts.Features.Physics.Characters.Movement.Components
{
    [Serializable]
    public struct WalkComponent
    {
        public bool Enabled;
        public float Force;
    }
}