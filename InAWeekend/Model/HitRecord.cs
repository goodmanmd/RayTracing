using System.Numerics;
using InAWeekend.Geometry;
using InAWeekend.Model.Materials;

namespace InAWeekend.Model
{
    internal readonly struct HitRecord
    {
        public Point3 P { get; }
        public Vector3 Normal { get; }
        public float T { get; }
        public bool FrontFace { get; }
        public IMaterial Material { get; }

        public HitRecord(Ray r, Point3 p, Vector3 outwardNormal, float t, IMaterial material)
        {
            P = p;
            T = t;
            FrontFace = r.Direction.Dot(outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
            Material = material;
        }
    }
}