namespace InAWeekend.Geometry
{
    interface IHittable
    {
        bool Hit(Ray r, float min, float max, out HitRecord hit);
    }
}