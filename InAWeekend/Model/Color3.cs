using System;

namespace InAWeekend.Model
{
    internal readonly struct Color3
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }

        public Color3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color3 operator +(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                Math.Min(lhs.R + rhs.R, 1.0f),
                Math.Min(lhs.G + rhs.G, 1.0f),
                Math.Min(lhs.B + rhs.B, 1.0f)
            );
        }

        public static Color3 operator -(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                Math.Max(lhs.R - rhs.R, 0.0f),
                Math.Max(lhs.G - rhs.G, 0.0f),
                Math.Max(lhs.B - rhs.B, 0.0f)
            );
        }

        public static Color3 operator *(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                Math.Min(lhs.R * rhs.R, 1.0f),
                Math.Min(lhs.G * rhs.G, 1.0f),
                Math.Min(lhs.B * rhs.B, 1.0f)
            );
        }

        public static Color3 operator *(Color3 lhs, float rhs)
        {
            return new Color3
            (
                Math.Min(lhs.R * rhs, 1.0f),
                Math.Min(lhs.G * rhs, 1.0f),
                Math.Min(lhs.B * rhs, 1.0f)
            );
        }

        public static Color3 operator *(float lhs, Color3 rhs)
        {
            return rhs * lhs;
        }

        public static readonly Color3 White = new Color3(1.0f, 1.0f, 1.0f);
        public static readonly Color3 Black = new Color3(0.0f, 0.0f, 0.0f);

        public static readonly Color3 Red = new Color3(1.0f, 0.0f, 0.0f);
        public static readonly Color3 Green = new Color3(0.0f, 1.0f, 0.0f);
        public static readonly Color3 Blue = new Color3(0.0f, 0.0f, 1.0f);

        public static readonly Color3 SkyBlue = new Color3(0.5f, 0.7f, 1.0f);
    }
}