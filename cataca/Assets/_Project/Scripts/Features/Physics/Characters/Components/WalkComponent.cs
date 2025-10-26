using System;

namespace _Project.Scripts.Features.Physics.Characters.Components
{
    [Serializable]
    public struct WalkComponent
    {
        public bool Enabled;
        public float Force;
    }
}