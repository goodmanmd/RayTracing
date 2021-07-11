using System.Numerics;

namespace InAWeekend.Geometry
{
    internal readonly struct Ray
    {
        public Point3 Origin { get; }
        public Vector3 Direction { get; }

        public Ray(Point3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3 At(float t)
        {
            return Origin + Vector3.Multiply(Direction, t);
        }
    }
}
