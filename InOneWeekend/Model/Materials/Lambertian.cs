using InOneWeekend.Geometry;
using InOneWeekend.Util;

namespace InOneWeekend.Model.Materials
{
    class Lambertian : IMaterial
    {
        private readonly Color3 _albedo;

        public Lambertian(Color3 albedo)
        {
            _albedo = albedo;
        }

        public bool Scatter(Ray rayInput, HitRecord hit, out Color3 attenuation, out Ray scatteredRay)
        {
            var scatterDirection = hit.Normal + RandomHelpers.NextNormalizedVector3();

            if (scatterDirection.NearZero()) scatterDirection = hit.Normal;

            scatteredRay = new Ray(hit.P, scatterDirection);
            attenuation = _albedo;

            return true;
        }
    }
}
