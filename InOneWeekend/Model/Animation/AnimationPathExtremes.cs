using InOneWeekend.Geometry;

namespace InOneWeekend.Model.Animation
{
    internal struct AnimationPathExtremes
    {
        public Point3 LocationAtT0 { get; set; }
        public Point3 LocationAtT1 { get; set; }
        public Point3 Minimums { get; set; }
        public Point3 Maximums { get; set; }
    }
}