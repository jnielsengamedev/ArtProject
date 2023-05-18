using UnityEngine;

namespace ArtProject.Data
{
    public static class Vector3Extensions
    {
        public static Vector3 SetScaleDirection(this Vector3 vector, HorizontalDirection direction)
        {
             return new Vector3(direction.FlipX(vector.x), vector.y, vector.z);
        }
    }
}