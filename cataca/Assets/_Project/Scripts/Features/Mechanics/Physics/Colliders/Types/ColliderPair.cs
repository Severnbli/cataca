using System;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Types
{
    public readonly struct ColliderPair : IEquatable<ColliderPair>
    {
        private readonly Collider2D _a;
        private readonly Collider2D _b;

        public ColliderPair(Collider2D a, Collider2D b)
        {
            if (a.GetHashCode() <= b.GetHashCode())
            {
                _a = a;
                _b = b;
            }
            else
            {
                _a = b;
                _b = a;
            }
        }

        public bool Equals(ColliderPair other) 
            => (_a == other._a && _b == other._b) || (_a == other._b && _b == other._a);
        
        public override bool Equals(object obj)
            => obj is ColliderPair other && Equals(other);

        public override int GetHashCode()
        {
            var hashA = _a != null ? _a.GetHashCode() : 0;
            var hashB = _b != null ? _b.GetHashCode() : 0;
            return HashCode.Combine(hashA < hashB ? hashA : hashB, hashA < hashB ? hashB : hashA);
        }
    }
}