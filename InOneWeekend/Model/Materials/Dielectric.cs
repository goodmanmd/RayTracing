using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Materials
{
    class Dielectric : IMaterial
    {
        private readonly float _indexOfRefraction;

        public Dielectric(float indexOfRefraction)
        {
            _indexOfRefraction = indexOfRefraction;
        }

        public bool Scatter(Ray rayInput, HitRecord hit, out Color3 attenuation, out Ray scatteredRay)
        {
            attenuation = Color3.White;
            var refractionRatio = hit.FrontFace 
                ? 1.0f / _indexOfRefraction 
                : _indexOfRefraction;

            var unitDirection = rayInput.Direction.Normalize();
            var refractedVector = unitDirection.Refract(hit.Normal, refractionRatio);

            scatteredRay = new Ray(hit.P, refractedVector, rayInput.Time);
            return true;
        }
    }
}
