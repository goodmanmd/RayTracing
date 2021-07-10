using System;
using System.Numerics;

namespace InAWeekend.Util
{
    public static class RandomHelpers
    {
        private static readonly Random GlobalRng = new Random();

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
            while (true)
            {
                var randomVec = NextVector3(-1.0f, 1.0f);
                if (randomVec.LengthSquared() >= 1) continue;

                return randomVec;
            }
        }
    }
}
