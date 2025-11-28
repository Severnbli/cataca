using System;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Types
{
    public readonly struct ColliderPair : IEquatable<ColliderPair>
    {
        public readonly Collider2D A;
        public readonly Collider2D B;

        public ColliderPair(Collider2D a, Collider2D b)
        {
            if (a.GetHashCode() <= b.GetHashCode())
            {
                A = a;
                B = b;
            }
            else
            {
                A = b;
                B = a;
            }
        }

        public bool Equals(ColliderPair other) 
            => (A == other.A && B == other.B) || (A == other.B && B == other.A);
        
        public override bool Equals(object obj)
            => obj is ColliderPair other && Equals(other);

        public override int GetHashCode()
        {
            var hashA = A != null ? A.GetHashCode() : 0;
            var hashB = B != null ? B.GetHashCode() : 0;
            return HashCode.Combine(hashA < hashB ? hashA : hashB, hashA < hashB ? hashB : hashA);
        }
    }
}