using System;
using System.Numerics;
using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Animation
{
    internal sealed class LinearAnimation : Animation
    {
        private readonly Vector3 _speed;

        public LinearAnimation(Vector3 speed)
        {
            _speed = speed;
        }

        public override Point3 Animate(Point3 center, float t) => center + _speed * t;

        public override AnimationPathExtremes GetExtremes(Point3 center, float t0, float t1)
        {
            var locationAtT0 = Animate(center, t0);
            var locationAtT1 = Animate(center, t1);

            var minX = Math.Min(locationAtT0.X, locationAtT1.X);
            var minY = Math.Min(locationAtT0.Y, locationAtT1.Y);
            var minZ = Math.Min(locationAtT0.Z, locationAtT1.Z);
            var maxX = Math.Max(locationAtT0.X, locationAtT1.X);
            var maxY = Math.Max(locationAtT0.Y, locationAtT1.Y);
            var maxZ = Math.Max(locationAtT0.Z, locationAtT1.Z);

            return new AnimationPathExtremes
            {
                LocationAtT0 = locationAtT0,
                LocationAtT1 = locationAtT1,
                Minimums = new Point3(minX, minY, minZ),
                Maximums = new Point3(maxX, maxY, maxZ)
            };
        }
    }
}