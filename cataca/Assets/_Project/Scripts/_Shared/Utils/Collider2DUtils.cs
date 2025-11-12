using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class Collider2DUtils
    {
        public static void DrawColliderAt(Collider2D collider, Transform transform, Color color)
        {
            SetMatrix(transform);
            DrawCollider(collider, color);
            ResetMatrix();
        }

        public static void DrawCollider(Collider2D collider, Color color)
        {
            var baseColor = Gizmos.color;
            Gizmos.color = color;
            
            switch (collider)
            {
                case BoxCollider2D box:
                {
                    DrawBox(box);
                    break;
                }
                case CircleCollider2D circle:
                {
                    DrawCircle(circle);
                    break;
                }
                case CapsuleCollider2D capsule:
                {
                    DrawCapsule(capsule);
                    break;
                }
                case PolygonCollider2D polygon:
                {
                    DrawPolygon(polygon);
                    break;
                }
                case CompositeCollider2D composite:
                {
                    DrawComposite(composite);
                    break;
                }
            }
            
            Gizmos.color = baseColor;
        }
        
        private static void SetMatrix(Transform transform)
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        }

        private static void ResetMatrix()
        {
            Gizmos.matrix = Matrix4x4.identity;
        }
        
        private static void DrawBox(BoxCollider2D box)
        {
            Gizmos.DrawWireCube(box.offset, box.size);
        }
        
        private static void DrawCircle(CircleCollider2D circle)
        {
            Gizmos.DrawWireSphere(circle.offset, circle.radius);
        }

        private static void DrawCapsule(CapsuleCollider2D capsule)
        {
            Gizmos.DrawWireCube(capsule.offset, capsule.size);
        }

        private static void DrawPolygon(PolygonCollider2D polygon)
        {
            var offset = (Vector3)polygon.offset;
            for (var p = 0; p < polygon.pathCount; p++)
            {
                var points = polygon.GetPath(p);
                for (var i = 0; i < points.Length; i++)
                {
                    var a = (Vector3)points[i] + offset;
                    var b = (Vector3)points[(i + 1) % points.Length] + offset;
                    Gizmos.DrawLine(a, b);
                }
            }
        }

        private static void DrawComposite(CompositeCollider2D composite)
        {
            var pathCount = composite.pathCount;
            for (var i = 0; i < pathCount; i++)
            {
                var pointCount = composite.GetPathPointCount(i);
                if (pointCount == 0) continue;

                var points = new Vector2[pointCount];
                composite.GetPath(i, points);

                for (var j = 0; j < pointCount; j++)
                {
                    var a = (Vector3)points[j];
                    var b = (Vector3)points[(j + 1) % pointCount];
                    Gizmos.DrawLine(a, b);
                }
            }
        }
    }
}