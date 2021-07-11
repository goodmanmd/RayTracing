using InAWeekend.Geometry;

namespace InAWeekend.Model.Materials
{
    class Metal : IMaterial
    {
        private readonly Color3 _albedo;

        public Metal(Color3 albedo)
        {
            _albedo = albedo;
        }

        public bool Scatter(Ray rayInput, HitRecord hit, out Color3 attenuation, out Ray scatteredRay)
        {
            var reflected = rayInput.Direction.Normalize().Reflect(hit.Normal);
            scatteredRay = new Ray(hit.P, reflected);
            attenuation = _albedo;

            return scatteredRay.Direction.Dot(hit.Normal) > 0;
        }
    }
}
