using System;
using InOneWeekend.Geometry;
using InOneWeekend.Model.Materials;

namespace InOneWeekend.Model
{
    struct Sphere : IHittable
    {
        public Point3 Center;
        public float Radius;
        public IMaterial Material;
        public Animation Animation;

        public Sphere(Point3 center, float radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
            Animation = AnimationType.None();
        }

        public Sphere(Point3 center, float radius, IMaterial material, Animation animation)
        {
            Center = center;
            Radius = radius;
            Material = material;
            Animation = animation;
        }

        public bool HitBy(Ray r, float min, float max, out HitRecord hit)
        {
            var centerAtRayTime = Animation(Center, r.Time);
            var centerVector = (r.Origin - centerAtRayTime).AsVector();
            var a = r.Direction.LengthSquared();
            var halfB = centerVector.Dot(r.Direction);
            var c = centerVector.LengthSquared() - Radius * Radius;
            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0)
            {
                hit = default;
                return false;
            }

            var sqrtD = (float)Math.Sqrt(discriminant);
            var root = (-halfB - sqrtD) / a;

            if (root < min || root > max)
            {
                root = (-halfB + sqrtD) / a;
                if (root < min || root > max)
                {
                    hit = default;
                    return false;
                }
            }

            var p = r.At(root);
            var outwardNormal = (p - centerAtRayTime).AsVector() / Radius;

            hit = new HitRecord(r, p, outwardNormal, root, Material);
            return true;
        }
    }
}
