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
        public (Vector2 position, Quaternion rotation) CalculateBoundaryMarker(float angleDegrees)
        {
            // normalize to [0, 360) interval
            float theta = (angleDegrees + 360f) % 360f;
            float thetaRad = theta * Mathf.Deg2Rad;

            // Unity space uses up as 0, so rotate instinctively from math space (cos(), sin())
            Vector2 direction = new(-Mathf.Sin(thetaRad), Mathf.Cos(thetaRad));

            // compute scale factor to bring larger component to +/- 1
            float scale = 1f / Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

            Vector2 projected = direction * scale;

            return (projected, Quaternion.Euler(0, 0, theta));
        }
    }
}