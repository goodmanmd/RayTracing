using System;
using System.Numerics;

namespace InAWeekend.Geometry
{
    public readonly struct Ray
    {
        public Point3 Origin { get; }
        public Vector3 Direction { get; }

        public Ray(Point3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3 At(float t)
        {
            return Origin + Vector3.Multiply(Direction, t);
        }
    }

    public static class RayExtensions
    {
        private static readonly Sphere ConstantSphere = new Sphere(new Point3(0, 0, -1), 0.5f);

        public static Color3 NormalColor(this Ray r)
        {
            var t = r.HitSphere(ConstantSphere);
            if (t > 0.0f)
            {
                var N = (r.At(t) + Vector3.UnitZ).AsVector().Normalize();
                return 0.5f * new Color3(N.X + 1, N.Y + 1, N.Z + 1);
            }

            var unitVector = r.Direction.Normalize();
            t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }

        public static float HitSphere(this Ray r, Sphere s)
        {
            var centerVector = (r.Origin - s.Center).AsVector();
            var a = r.Direction.LengthSquared();
            var halfB = centerVector.Dot(r.Direction);
            var c = centerVector.LengthSquared() - Math.Pow(s.Radius, 2);
            var discriminant = halfB*halfB - a * c;

            return discriminant < 0
                ? -1.0f
                : (float)((-halfB - Math.Sqrt(discriminant)) / a);
        }
    }
}
