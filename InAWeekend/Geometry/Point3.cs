using System;
using System.Globalization;
using System.Numerics;
using System.Text;

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

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var sb = new StringBuilder();
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            sb.Append('<');
            sb.Append(X.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Y.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Z.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }
    }
}