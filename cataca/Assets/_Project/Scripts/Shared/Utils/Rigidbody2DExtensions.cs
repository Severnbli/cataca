using UnityEngine;

namespace _Project.Scripts.Shared.Utils
{
    public static class Rigidbody2DExtensions
    {
        public static void AddForce(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            rigidbody2D.linearVelocity += direction.normalized * force;
        }

        public static void ProcessWalk(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            var walkDirection = new Vector2(direction.x, 0f);
            rigidbody2D.AddForce(force, walkDirection);
        }

        public static void ProcessJump(this Rigidbody2D rigidbody2D, float force)
        {
            rigidbody2D.AddForce(force, Vector2.up);
        }

        public static void ProcessDash(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            rigidbody2D.AddForce(force, direction);
        }
    }
}