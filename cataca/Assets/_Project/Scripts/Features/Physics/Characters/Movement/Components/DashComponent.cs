using System;

namespace _Project.Scripts.Features.Physics.Characters.Movement.Components
{
    [Serializable]
    public struct DashComponent
    {
        public bool Enabled;
        public float Force;
        public int MaxCount;
        public float Duration;
        [NonSerialized] public int CurrentCount;
    }
}