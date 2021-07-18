using System;
using System.Numerics;
using InAWeekend.Util;

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

        public static Vector3 Refract(this Vector3 vector, Vector3 normal, float refractionRatio)
        {
            var cosTheta = Math.Min(1.0f, -vector.Dot(normal));
            var sinTheta = (float)Math.Sqrt(1.0f - (cosTheta * cosTheta));

            //In the "In a Weekend" guide, this calculation is actually performed in the Dielectric class,
            //but that duplicates the cos(theta) calculation. I think it can make sense here: this behavior
            //seems to be a physical law governing refaction/reflection and not the material itself.
            if (refractionRatio * sinTheta > 1.0 || Reflectance(cosTheta, refractionRatio) > RandomHelpers.NextFloat())
            {
                return vector.Reflect(normal);
            }

            var rOutPerpendicular = refractionRatio * (vector + cosTheta * normal);
            var rOutParallel = -(float)Math.Sqrt(Math.Abs(1.0 - rOutPerpendicular.LengthSquared())) * normal;

            return rOutPerpendicular + rOutParallel;
        }

        private static float Reflectance(float cosTheta, float indexOfRefraction)
        {
            var r0 = Math.Pow((1 - indexOfRefraction) / (1 + indexOfRefraction), 2);
            return (float)(r0 + (1 - r0) * Math.Pow(1 - cosTheta, 5));
        }
    }
}