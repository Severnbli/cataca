using _Project.Scripts.Features.Physics.Characters.Components;
using UnityEngine;

namespace _Project.Scripts.Shared.Utils
{
    public static class Rigidbody2DExtensions
    {
        public static void ProcessWalk(this Rigidbody2D rigidbody2D, float force, float walkFactor)
        {
            rigidbody2D.linearVelocity = new Vector2(force * walkFactor, rigidbody2D.linearVelocity.y);
        }
    }
}