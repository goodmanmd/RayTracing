using System;
using System.Numerics;

namespace InAWeekend.Geometry
{
    internal readonly struct Point3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Point3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public static Point3 operator +(Point3 lhs, Vector3 rhs)
        {
            return new Point3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Point3 operator -(Point3 lhs, Point3 rhs)
        {
            return new Point3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Point3 operator -(Point3 lhs, Vector3 rhs)
        {
            return new Point3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public Vector3 AsVector() => new Vector3(X, Y, Z);
    }
}