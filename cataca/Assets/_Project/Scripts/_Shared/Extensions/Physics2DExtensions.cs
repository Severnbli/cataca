using UnityEngine;

namespace _Project.Scripts._Shared.Extensions
{
    public static class Physics2DExtensions
    {
        public static bool IsHitCollider(Vector2 position, Vector2 vector2, float distance, LayerMask layerMask)
        {
            var hit = Physics2D.Raycast(position, vector2, distance, layerMask);
            return hit.collider is not null;
        }
    }
}