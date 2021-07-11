using System;
using System.Numerics;

namespace InAWeekend.Geometry
{
    public static class Vector3Extensions
    {
        public static float Dot(this Vector3 lhs, Vector3 rhs) => Vector3.Dot(lhs, rhs);

        public static bool NearZero(this Vector3 vector)
        {
            const float tolerance = 1e-8f;
            return Math.Abs(vector.X) < tolerance
                   && Math.Abs(vector.Y) < tolerance
                   && Math.Abs(vector.Z) < tolerance;
        }

        public static Vector3 Normalize(this Vector3 lhs) => Vector3.Normalize(lhs);

        public static Vector3 Reflect(this Vector3 vector, Vector3 normal)
        {
            return Vector3.Reflect(vector, normal);
        }
    }
}