using UnityEngine;

namespace Utils
{
    public class GizmosUtils
    {
        public static void DrawCircle(Vector2 center, float radius, Color color, int corners = 12)
        {
            var startRotation = Vector2.right * radius; // Where the first point of the circle starts
            var lastPosition = center + startRotation;
            float angle = 0;
            var oldColor = Gizmos.color;
            Gizmos.color = color;
            while (angle <= 360)
            {
                angle += (float)360 / corners;
                var nextPosition = center + (Vector2)(Quaternion.Euler(0, 0, angle) * startRotation);
                Gizmos.DrawLine(lastPosition, nextPosition);

                lastPosition = nextPosition;
            }
            Gizmos.color = oldColor;
        }
    }
}