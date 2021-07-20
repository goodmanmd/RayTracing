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

        private float LensRadius { get; }

        public Camera(
            Point3 lookFrom,
            Point3 lookAt,
            Vector3 vUp,
            float verticalFieldOfViewInDegrees, 
            float aspectRatio,
            float aperture,
            float focusDistance)
        {
            var theta = MathUtil.DegreesToRadians(verticalFieldOfViewInDegrees);
            var h = (float)Math.Tan(theta / 2);
            var viewportHeight = 2 * h;
            var viewportWidth = viewportHeight * aspectRatio;

            W = (lookFrom - lookAt).AsVector().Normalize();
            U = vUp.Cross(W).Normalize();
            V = W.Cross(U);

            Origin = lookFrom;
            Horizontal = focusDistance * viewportWidth * U;
            Vertical = focusDistance * viewportHeight * V;
            LowerLeftCorner = Origin - Horizontal / 2.0f - Vertical / 2.0f - focusDistance*W;

            LensRadius = aperture / 2.0f;
        }

        public Ray GetRay(float s, float t)
        {
            var rd = LensRadius * RandomHelpers.NextVector3InUnitDisk();
            var offset = U * rd.X + V * rd.Y;

            return new Ray
            (
                Origin + offset,
                LowerLeftCorner.AsVector() + s * Horizontal + t * Vertical - Origin.AsVector() - offset
            );
        }
    }
}