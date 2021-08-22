using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Animation
{
    internal abstract class Animation
    {
        public abstract Point3 Animate(Point3 center, float t);
        public abstract AnimationPathExtremes GetExtremes(Point3 center, float t0, float t1);
    }
}