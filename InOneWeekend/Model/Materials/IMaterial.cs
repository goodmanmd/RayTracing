using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Materials
{
    interface IMaterial
    {
        bool Scatter(Ray rayInput, HitRecord hit, out Color3 attenuation, out Ray scatteredRay);
    }
}
