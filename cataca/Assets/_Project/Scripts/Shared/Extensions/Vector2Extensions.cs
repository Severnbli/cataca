using UnityEngine;

namespace _Project.Scripts.Shared.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 GetNearestDiagonal(this Vector2 vector)
        {
            var result = new Vector2(Mathf.Sign(vector.x), Mathf.Sign(vector.y));
            return result.normalized;
        }
    }
}