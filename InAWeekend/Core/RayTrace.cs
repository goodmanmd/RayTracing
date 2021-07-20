using InAWeekend.Geometry;
using InAWeekend.Model;

namespace InAWeekend.Core
{
    static class RayTrace
    {
        public static Color3 Trace(this Ray r, Scene scene, int depth)
        {
            if (depth <= 0) return Color3.Black;

            var rayIntersectsScene = scene.HitBy(r, 0.001f, float.MaxValue, out var hit);

            if (rayIntersectsScene)
            {
                if (hit.Material.Scatter(r, hit, out var attenuation, out var scatteredRay))
                {
                    return attenuation * scatteredRay.Trace(scene, depth - 1);
                }

                return Color3.Black;
            }

            var unitVector = r.Direction.Normalize();
            var t = 0.5f * (unitVector.Y + 1.0f);

            return (1.0f - t) * Color3.White + t*Color3.SkyBlue;
        }
    }
}