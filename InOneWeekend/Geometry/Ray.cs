using System.Numerics;

namespace InOneWeekend.Geometry
{
    internal readonly struct Ray
    {
        public Point3 Origin { get; }
        public Vector3 Direction { get; }
        public float Time { get; }

        public Ray(Point3 origin, Vector3 direction, float time)
        {
            Origin = origin;
            Direction = direction;
            Time = time;
        }

        public Point3 At(float t)
        {
            return Origin + Vector3.Multiply(Direction, t);
        }
    }
}
