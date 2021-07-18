using System;
using System.Numerics;
using InAWeekend.Geometry;

namespace InAWeekend.Util
{
    public static class RandomHelpers
    {
        private static readonly Random GlobalRng = new Random();

        public static float NextFloat() => GlobalRng.NextFloat();
        public static float NextFloat(this Random rng) => (float)rng.NextDouble();

        public static float NextFloat(this Random rng, float min, float max)
        {
            return min + (max - min)*rng.NextFloat();
        }

        public static Vector3 NextVector3()
        {
            return new Vector3(GlobalRng.NextFloat(), GlobalRng.NextFloat(), GlobalRng.NextFloat());
        }

        public static Vector3 NextVector3(float min, float max)
        {
            return new Vector3(GlobalRng.NextFloat(min, max), GlobalRng.NextFloat(min, max), GlobalRng.NextFloat(min, max));
        }

        public static Vector3 NextVector3InUnitSphere()
        {
            var u = GlobalRng.NextDouble();
            var v = GlobalRng.NextDouble();
            var theta = u * 2.0 * Math.PI;
            var phi = Math.Acos(2.0 * v - 1);
            var r = Math.Cbrt(GlobalRng.NextDouble());
            var sinTheta = Math.Sin(theta);
            var cosTheta = Math.Cos(theta);
            var sinPhi = Math.Sin(phi);
            var cosPhi = Math.Cos(phi);

            var x = (float)(r * sinPhi * cosTheta);
            var y = (float)(r * sinPhi * sinTheta);
            var z = (float)(r * cosPhi);

            return new Vector3(x, y, z);
        }

        public static Vector3 NextNormalizedVector3()
        {
            return NextVector3InUnitSphere().Normalize();
        }
    }
}
