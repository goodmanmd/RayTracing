using InOneWeekend.Geometry;

namespace InOneWeekend.Model
{
    interface IHittable
    {
        bool HitBy(Ray r, float min, float max, out HitRecord hit);
    }
}