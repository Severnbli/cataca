using UnityEngine;

namespace _Project.Scripts.Shared.Utils
{
    public static class Rigidbody2DExtensions
    {
        public static void ProcessWalk(this Rigidbody2D rigidbody2D, float force, float walkFactor)
        {
            rigidbody2D.linearVelocity = new Vector2(force * walkFactor, rigidbody2D.linearVelocity.y);
        }

        public static void ProcessPush(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            rigidbody2D.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        }

        public static void ProcessUpperPush(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            var upperDirection = new Vector2(direction.x, Mathf.Abs(direction.y));
            rigidbody2D.ProcessPush(force, upperDirection);
        }
    }
}