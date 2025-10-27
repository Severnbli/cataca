using System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Characters.Checks.Components
{
    [Serializable]
    public struct GroundCheckComponent
    {
        public bool Disabled;
        public Transform Transform;
        public LayerMask Layer;
        public float Distance;
    }
}