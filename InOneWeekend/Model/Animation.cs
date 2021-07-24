using System.Numerics;
using InOneWeekend.Geometry;

namespace InOneWeekend.Model
{
    internal delegate Point3 Animation(Point3 center, float t);

    public static class AnimationType
    {
        internal static Animation None() => (c, t) => c;

        internal static Animation Linear(Point3 center, Vector3 speed) =>
            (c, t) => center + speed * t;
    }
}