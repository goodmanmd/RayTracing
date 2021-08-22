using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Animation
{
    internal sealed class NoAnimation : Animation
    {
        public static NoAnimation Instance { get; } = new NoAnimation();
        
        public override Point3 Animate(Point3 center, float t)
        {
            return center;
        }

        public override AnimationPathExtremes GetExtremes(Point3 center, float t0, float t1)
        {
            return new AnimationPathExtremes
            {
                Minimums = center,
                Maximums = center
            };
        }
    }
}