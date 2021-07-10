using System;

namespace InAWeekend.Geometry
{
    class Sphere : IHittable
    {
        public Point3 Center { get; }
        public float Radius { get; }

        public Sphere(Point3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Hit(Ray r, float min, float max, out HitRecord hit)
        {
            var centerVector = (r.Origin - Center).AsVector();
            var a = r.Direction.LengthSquared();
            var halfB = centerVector.Dot(r.Direction);
            var c = centerVector.LengthSquared() - Math.Pow(Radius, 2);
            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0)
            {
                hit = default;
                return false;
            }

            var sqrtD = Math.Sqrt(discriminant);
            var root = (float)(-halfB - sqrtD) / a;

            if (root < min || root > max)
            {
                root = (float)(-halfB + sqrtD) / a;
                if (root < min || root > max)
                {
                    hit = default;
                    return false;
                }
            }

            var p = r.At(root);
            var outwardNormal = (p - Center).AsVector() / Radius;

            hit = new HitRecord(r, p, outwardNormal, root);
            return true;
        }
    }
}
