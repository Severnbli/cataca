using System;

namespace _Project.Scripts.Features.Physics.Characters.Components
{
    [Serializable]
    public struct JumpComponent
    {
        public bool Enabled;
        public float Force;
        public int MaxCount;
        [NonSerialized] public int CurrentCount;
    }
}