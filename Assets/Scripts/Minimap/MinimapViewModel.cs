using UnityEngine;

namespace Core.Minimap
{
    public class MinimapViewModel
    {


        /// <summary>
        /// Return coordinate position and rotation of a marker anchored to the minimap boundary
        /// </summary>
        /// <param name="angleDegrees">Angle between object rotation and reference axis</param>
        /// <returns>A Vector2 normalized coordinate position and Quaternion rotation</returns>
        private (Vector2 position, Quaternion rotation) CalculateBoundaryMarker(float angleDegrees)
        {
            // minimap consists of 4 sectors: (45, 135), (135, 225), (225, 315), and (315, 45)
            // watch out for asymptotes at 45, 135, 225, and 315 - use theta +/- 0.5 deg to clamp result

            // threshold for boundary checks
            const float epsilon = 0.5f;

            // normalize to [0, 360) interval
            float theta = (angleDegrees + 360f) % 360f;

            if (Mathf.Abs(theta - 45f) < epsilon) // top left
                return (new Vector2(-1, 1), Quaternion.Euler(0, 0, 45));
            if (Mathf.Abs(theta - 135f) < epsilon) // bottom left
                return (new Vector2(-1, -1), Quaternion.Euler(0, 0, 135));
            if (Mathf.Abs(theta - 225f) < epsilon) // bottom right
                return (new Vector2(1, -1), Quaternion.Euler(0, 0, 225));
            if (Mathf.Abs(theta - 315f) < epsilon) // top right
                return (new Vector2(1, 1), Quaternion.Euler(0, 0, 315));

            if (theta < 45 || theta > 315)
            {
                float x = 1f / Mathf.Tan(theta * Mathf.Deg2Rad);
                return (new Vector2(Mathf.Clamp(x, -1, 1), 1), Quaternion.Euler(0, 0, 0));
            }
            if (theta >= 45 && theta < 135)
            {
                float y = -Mathf.Tan(theta * Mathf.Deg2Rad);
                return (new Vector2(-1, Mathf.Clamp(y, -1, 1)), Quaternion.Euler(0, 0, 90));
            }
            if (theta >= 135 && theta < 225)
            {
                float x = -1 / Mathf.Tan(theta * Mathf.Deg2Rad);
                return (new Vector2(Mathf.Clamp(x, -1, 1), -1), Quaternion.Euler(0, 0, 180));
            }
            if (theta >= 225 && theta < 315)
            {
                float y = Mathf.Tan(theta * Mathf.Deg2Rad);
                return (new Vector2(1, Mathf.Clamp(y, -1, 1)), Quaternion.Euler(0, 0, 270));
            }

            return (new Vector2(0, 1), Quaternion.identity);
        }
    }
}