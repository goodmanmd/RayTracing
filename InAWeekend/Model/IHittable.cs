using InAWeekend.Geometry;

namespace InAWeekend.Model
{
    interface IHittable
    {
        bool Hit(Ray r, float min, float max, out HitRecord hit);
    }
}