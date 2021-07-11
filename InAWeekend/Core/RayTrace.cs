using InAWeekend.Geometry;
using InAWeekend.Model;
using InAWeekend.Util;

namespace InAWeekend.Core
{
    static class RayTrace
    {
        public static Color3 Trace(this Ray r, Scene scene, int depth)
        {
            if (depth <= 0) return Color3.Black;

            var rayIntersectsScene = scene.Hit(r, 0.001f, float.MaxValue, out var hit);

            if (rayIntersectsScene)
            {
                //diffuse reflection
                var target = hit.P + hit.Normal + RandomHelpers.NextNormalizedVector3();
                var reflectedRay = new Ray(hit.P, (target - hit.P).AsVector());
                return 0.5f * reflectedRay.Trace(scene, depth - 1);
            }

            var unitVector = r.Direction.Normalize();
            var t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }
    }
}