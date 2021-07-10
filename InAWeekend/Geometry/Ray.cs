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
            if (r.HitSphere(ConstantSphere))
            {
                return Color3.Red;
            }

            var unitVector = Vector3.Normalize(r.Direction);
            var t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }

        public static bool HitSphere(this Ray r, Sphere s)
        {
            var centerVector = (r.Origin - s.Center).AsVector();
            var a = r.Direction.Dot(r.Direction);
            var b = 2.0 * centerVector.Dot(r.Direction);
            var c = centerVector.Dot(centerVector) - Math.Pow(s.Radius, 2);
            var discriminant = b * b - 4 * a * c;

            return discriminant > 0;
        }
    }
}
