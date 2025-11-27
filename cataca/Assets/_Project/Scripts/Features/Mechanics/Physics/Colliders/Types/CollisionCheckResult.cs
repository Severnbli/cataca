using _Project.Scripts.Features.Mechanics.Physics.Colliders.Enums;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Types
{
    public struct CollisionCheckResult
    {
        public CollisionCheckStatus CheckStatus;
        public ColliderDistance2D Distance;
    }
}