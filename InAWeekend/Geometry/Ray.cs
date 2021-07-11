using System.Numerics;
using InAWeekend.Util;

namespace InAWeekend.Geometry
{
    internal readonly struct Ray
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

    static class RayExtensions
    {
        public static Color3 Trace(this Ray r, Scene scene, int depth)
        {
            if (depth <= 0) return Color3.Black;

            var rayIntersectsScene = scene.Hit(r, 0.001f, float.MaxValue, out var hit);

            if (rayIntersectsScene)
            {
                //diffuse reflection
                var target = hit.P + hit.Normal + RandomHelpers.NextVector3InUnitSphere();
                var reflectedRay = new Ray(hit.P, (target - hit.P).AsVector());
                return 0.5f * reflectedRay.Trace(scene, depth - 1);
            }

            var unitVector = r.Direction.Normalize();
            var t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }
    }
}
