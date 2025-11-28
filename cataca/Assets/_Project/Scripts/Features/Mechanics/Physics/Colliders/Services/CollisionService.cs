using System.Collections.Generic;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Services
{
    public sealed class CollisionService
    {
        public Dictionary<ColliderPair, CollisionCheckResult> Collisions { get; private set; } = new();
    }
}