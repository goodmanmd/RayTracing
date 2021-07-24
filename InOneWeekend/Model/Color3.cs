using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using InOneWeekend.Util;

namespace InOneWeekend.Model
{
    internal struct Color3
    {
        public float R;
        public float G;
        public float B;

        public Color3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color3 operator +(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                lhs.R + rhs.R,
                lhs.G + rhs.G,
                lhs.B + rhs.B
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color3 operator -(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                lhs.R - rhs.R,
                lhs.G - rhs.G,
                lhs.B - rhs.B
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color3 operator *(Color3 lhs, Color3 rhs)
        {
            return new Color3
            (
                lhs.R * rhs.R,
                lhs.G * rhs.G,
                lhs.B * rhs.B
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color3 operator *(Color3 lhs, float rhs)
        {
            return new Color3
            (
                lhs.R * rhs,
                lhs.G * rhs,
                lhs.B * rhs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color3 operator *(float lhs, Color3 rhs)
        {
            return rhs * lhs;
        }

        public static implicit operator Color(Color3 color)
        {
            var r = MathUtil.Clamp((int) (255 * color.R), 0, 255);
            var g = MathUtil.Clamp((int)(255 * color.G), 0, 255);
            var b = MathUtil.Clamp((int)(255 * color.B), 0, 255);

            return Color.FromArgb(r, g, b);
        }

        public static Color3 Random(Random rng)
        {
            return new Color3(rng.NextFloat(), rng.NextFloat(), rng.NextFloat());
        }

        public static Color3 Random(Random rng, float min, float max)
        {
            return new Color3(rng.NextFloat(min, max), rng.NextFloat(min, max), rng.NextFloat(min, max));
        }

        public static readonly Color3 White = new Color3(1.0f, 1.0f, 1.0f);
        public static readonly Color3 Black = new Color3(0.0f, 0.0f, 0.0f);

        public static readonly Color3 Red = new Color3(1.0f, 0.0f, 0.0f);
        public static readonly Color3 Green = new Color3(0.0f, 1.0f, 0.0f);
        public static readonly Color3 Blue = new Color3(0.0f, 0.0f, 1.0f);

        public static readonly Color3 SkyBlue = new Color3(0.5f, 0.7f, 1.0f);
    }
}