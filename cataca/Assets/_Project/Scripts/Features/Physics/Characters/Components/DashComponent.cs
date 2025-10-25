using System;

namespace _Project.Scripts.Features.Physics.Characters.Components
{
    [Serializable]
    public struct DashComponent
    {
        public bool Enabled;
        public float Force;
        public int MaxCount;
    }
}