using System;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics._Shared.Components
{
    [Serializable]
    public struct RigidbodyComponent
    {
        public Rigidbody2D Rigidbody;
        [NonSerialized] public float BaseXVelocity;
        [NonSerialized] public float AdditiveXVelocity;
    }
}