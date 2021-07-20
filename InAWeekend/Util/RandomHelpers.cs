using System;
using System.Numerics;
using InAWeekend.Geometry;

namespace InAWeekend.Util
{
    public static class RandomHelpers
    {
        public static float NextFloat(this Random rng) => (float)rng.NextDouble();
        public static float NextFloat(this Random rng, float min, float max) => min + (max-min)*(float)rng.NextDouble();

        public static Vector3 NextVector3InUnitSphere()
        {
            //see https://karthikkaranth.me/blog/generating-random-points-in-a-sphere/
            var u = ThreadLocalRandom.Instance.NextDouble();
            var v = ThreadLocalRandom.Instance.NextDouble();
            var theta = u * 2.0 * Math.PI;
            var phi = Math.Acos(2.0 * v - 1);
            var r = Math.Cbrt(ThreadLocalRandom.Instance.NextDouble());
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

        public static Vector3 NextVector3InUnitDisk()
        {
            while (true)
            {
                var p = new Vector3
                (
                    ThreadLocalRandom.Instance.NextFloat(-1.0f, 1.0f),
                    ThreadLocalRandom.Instance.NextFloat(-1.0f, 1.0f),
                    0
                );

                if (p.LengthSquared() >= 1.0f) continue;
                return p;
            }
        }
    }
}
