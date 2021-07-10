using System;
using InAWeekend.Geometry;

namespace InAWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        private readonly Random _rng = new Random();

        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer, int samplesPerPixel)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    float r = 0, g = 0, b = 0;

                    for (var sample = 0; sample < samplesPerPixel; sample++)
                    {
                        var u = (float)(i + _rng.NextDouble()) / (imageWidth - 1);
                        var v = (float)(j + _rng.NextDouble()) / (imageHeight - 1);

                        var ray = camera.GetRay(u, v);

                        var pixelColor = ray.Trace(scene);

                        r += pixelColor.R;
                        g += pixelColor.G;
                        b += pixelColor.B;
                    }

                    frameBuffer[i, j] = new Color3
                    (
                        NormalizeColor(r, samplesPerPixel),
                        NormalizeColor(g, samplesPerPixel),
                        NormalizeColor(b, samplesPerPixel)
                    );
                }
            }
        }

        private float NormalizeColor(float value, int numberOfSamples)
        {
            return Math.Max(0.0f, Math.Min(1.0f, value / numberOfSamples));
        }
    }
}
