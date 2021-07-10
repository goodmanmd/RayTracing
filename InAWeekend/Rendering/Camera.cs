using System.Numerics;
using InAWeekend.Geometry;

namespace InAWeekend.Rendering
{
    class Camera
    {
        public float ViewportHeight { get; }
        public float ViewportWidth { get; }
        public float AspectRatio { get; }
        public float FocalLength { get; }

        public Point3 Origin { get; }

        public Vector3 Vertical { get; }
        public Vector3 Horizontal { get; }
        
        public Point3 LowerLeftCorner { get; }

        public Camera()
        {
            ViewportHeight = 2.0f;
            AspectRatio = 16.0f / 9.0f;
            FocalLength = 1.0f;

            Origin = new Point3(0, 0, 0);
        }

        public Camera(float viewportHeight, float aspectRatio, float focalLength, Point3 origin = default)
        {
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportHeight * aspectRatio;
            AspectRatio = aspectRatio;
            FocalLength = focalLength;
            Origin = origin;

            Vertical = new Vector3(0, ViewportHeight, 0);
            Horizontal = new Vector3(ViewportWidth, 0, 0);

            LowerLeftCorner = origin - Horizontal/2.0f - Vertical/2.0f - new Vector3(0, 0, FocalLength);
        }
    }
}