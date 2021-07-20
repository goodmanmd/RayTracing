using System;
using System.Numerics;
using InAWeekend.Geometry;
using InAWeekend.Util;

namespace InAWeekend.Rendering
{
    class Camera
    {
        private Point3 Origin { get; }
        private Point3 LowerLeftCorner { get; }

        private Vector3 Vertical { get; }
        private Vector3 Horizontal { get; }

        private Vector3 U { get; }
        private Vector3 V { get; }
        private Vector3 W { get; }

        public Camera() 
            : this(
                new Point3(0, 0, -1), 
                new Point3(0, 0, 0), 
                Vector3.UnitY, 
                40, 
                1)
        {
        }

        public Camera(
            Point3 lookFrom,
            Point3 lookAt,
            Vector3 vUp,
            float verticalFieldOfViewInDegrees, 
            float aspectRatio)
        {
            var theta = MathUtil.DegreesToRadians(verticalFieldOfViewInDegrees);
            var h = (float)Math.Tan(theta / 2);
            var viewportHeight = 2 * h;
            var viewportWidth = viewportHeight * aspectRatio;

            W = (lookFrom - lookAt).AsVector().Normalize();
            U = vUp.Cross(W);
            V = W.Cross(U);

            Origin = lookFrom;
            Horizontal = viewportWidth * U;
            Vertical = viewportHeight * V;
            LowerLeftCorner = Origin - Horizontal / 2.0f - Vertical / 2.0f - W;
        }

        public Ray GetRay(float s, float t)
        {
            return new Ray
            (
                Origin, 
                LowerLeftCorner.AsVector() + s * Horizontal + t * Vertical - Origin.AsVector()
            );
        }
    }
}