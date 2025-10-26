using UnityEngine;

namespace _Project.Scripts.Shared.Utils
{
    public static class Vector2Extensions
    {
        public static Vector2 GetNearestDiagonal(this Vector2 vector)
        {
            var result = new Vector2(Mathf.Sign(vector.x), Mathf.Sign(vector.y));
            return result.normalized;
        }

        public static Vector2 AddVerticalForce(this Vector2 vector, float force)
        {
            vector.y += force;
            return vector;
        }
    }
}