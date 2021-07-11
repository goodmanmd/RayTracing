using InAWeekend.Geometry;
using InAWeekend.Util;

namespace InAWeekend.Model.Materials
{
    class Metal : IMaterial
    {
        private readonly Color3 _albedo;
        private readonly float _fuzzFactor;

        public Metal(Color3 albedo, float fuzzFactor)
        {
            _albedo = albedo;
            _fuzzFactor = fuzzFactor < 1 ? fuzzFactor : 1.0f;
        }

        public bool Scatter(Ray rayInput, HitRecord hit, out Color3 attenuation, out Ray scatteredRay)
        {
            var reflected = rayInput.Direction.Normalize().Reflect(hit.Normal);
            scatteredRay = new Ray(hit.P, reflected + _fuzzFactor * RandomHelpers.NextVector3InUnitSphere());
            attenuation = _albedo;

            return scatteredRay.Direction.Dot(hit.Normal) > 0;
        }
    }
}
