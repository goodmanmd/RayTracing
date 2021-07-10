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
    }
}
