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
        public static Color3 NormalColor(this Ray r)
        {
            var unitVector = Vector3.Normalize(r.Direction);
            var t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }
    }
}
