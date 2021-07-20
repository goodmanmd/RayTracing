using InAWeekend.Geometry;

namespace InAWeekend.Model
{
    interface IHittable
    {
        bool HitBy(Ray r, float min, float max, out HitRecord hit);
    }
}