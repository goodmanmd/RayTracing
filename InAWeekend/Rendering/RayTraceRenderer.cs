using System;
using InAWeekend.Geometry;
using InAWeekend.Util;

namespace InAWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        private readonly int _samplesPerPixel;
        private readonly int _maxRecurseDepth;
        private readonly Random _rng = new Random();

        public RayTraceRenderer(int samplesPerPixel, int maxRecurseDepth)
        {
            _samplesPerPixel = samplesPerPixel;
            _maxRecurseDepth = maxRecurseDepth;
        }

        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    float r = 0, g = 0, b = 0;

                    for (var sample = 0; sample < _samplesPerPixel; sample++)
                    {
                        var u = (i + _rng.NextFloat()) / (imageWidth - 1);
                        var v = (j + _rng.NextFloat()) / (imageHeight - 1);

                        var ray = camera.GetRay(u, v);

                        var pixelColor = ray.Trace(scene, _maxRecurseDepth);

                        r += pixelColor.R;
                        g += pixelColor.G;
                        b += pixelColor.B;
                    }

                    frameBuffer[i, j] = new Color3
                    (
                        NormalizeColor(r, _samplesPerPixel),
                        NormalizeColor(g, _samplesPerPixel),
                        NormalizeColor(b, _samplesPerPixel)
                    );
                }

                Console.WriteLine($"{j} of {frameBuffer.Height} scan lines remaining");
            }
        }

        private float NormalizeColor(float value, int numberOfSamples)
        {
            return Math.Max(0.0f, Math.Min(1.0f, value / numberOfSamples));
        }
    }
}
