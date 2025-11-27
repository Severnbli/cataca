using System.Collections.Generic;
using _Project.Scripts.Features.Mechanics.Physics.Colliders.Types;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Physics.Colliders.Services
{
    public sealed class CollisionService
    {
        public Dictionary<KeyValuePair<Collider2D, Collider2D>, CollisionCheckResult> Collisions { get; private set; } = new();
    }
}