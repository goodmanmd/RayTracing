using System.Numerics;

namespace InAWeekend.Geometry
{
    readonly struct HitRecord
    {
        public Point3 P { get; }
        public Vector3 Normal { get; }
        public float T { get; }
        public bool FrontFace { get; }

        public HitRecord(Ray r, Point3 p, Vector3 outwardNormal, float t)
        {
            P = p;
            T = t;
            FrontFace = r.Direction.Dot(outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }
    }
}