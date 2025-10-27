using System;

namespace _Project.Scripts.Features.Physics.Characters.Checks.Components
{
    public struct GroundCheckTimeBlockComponent
    {
        public float BlockTime;
        [NonSerialized] public float ElapsedTime;
    }
}