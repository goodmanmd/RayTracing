using System.Numerics;

namespace InAWeekend.Geometry
{
    public static class Vector3Extensions
    {
        public static float Dot(this Vector3 lhs, Vector3 rhs) => Vector3.Dot(lhs, rhs);
        public static Vector3 Normalize(this Vector3 lhs) => Vector3.Normalize(lhs);
    }
}