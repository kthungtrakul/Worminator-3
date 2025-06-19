using System;
using UnityEngine;

namespace Core.Minimap
{
    public class MinimapViewModel
    {
        private float _minimapSize = 0;

        public void SetMinimapSize(float mapSize)
        {
            _minimapSize = mapSize;
        }

        public Vector2 CalculateOnMapMarker(float offsetX, float offsetZ)
        {
            float absX = Mathf.Abs(offsetX);
            float absZ = Mathf.Abs(offsetZ);

            if (absX > _minimapSize && absZ > _minimapSize)
                throw new InvalidOperationException("Offset exceeds map boundary. Please use off-map logic.");
            if (absX > _minimapSize && absZ <= _minimapSize)
                return new(Mathf.Sign(offsetX), offsetZ / _minimapSize);
            if (absZ > _minimapSize && absX <= _minimapSize)
                return new(offsetX / _minimapSize, Mathf.Sign(offsetZ));

            return new(offsetX / _minimapSize, offsetZ / _minimapSize);
        }

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