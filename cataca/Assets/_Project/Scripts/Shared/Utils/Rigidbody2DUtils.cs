using UnityEngine;

namespace _Project.Scripts.Shared.Utils
{
    public static class Rigidbody2DUtils
    {
        public static void AddForce(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            rigidbody2D.linearVelocity += direction.normalized * force;
        }
        
        public static void AddHorizontalForce(this Rigidbody2D rigidbody2D, float force)
        {
            rigidbody2D.linearVelocity += Vector2.right * force;
        }

        public static void InstallHorizontalForce(this Rigidbody2D rigidbody2D, float force, Vector2 direction)
        {
            rigidbody2D.linearVelocity = new Vector2(direction.normalized.x * force, rigidbody2D.linearVelocity.y);
        }

        public static void AddVerticalForce(this Rigidbody2D rigidbody2D, float force)
        {
            rigidbody2D.linearVelocity += Vector2.up * force;
        }
    }
}