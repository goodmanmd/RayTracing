using System;
using System.Threading;
using System.Threading.Tasks;
using InAWeekend.Core;
using InAWeekend.Model;
using InAWeekend.Util;

namespace InAWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        private readonly int _samplesPerPixel;
        private readonly int _maxRecurseDepth;
        private readonly Random _rng = new Random();

        private int _pixelsRendered = 0;
        private int _pixelsInRender = 0;

        public RayTraceRenderer(int samplesPerPixel, int maxRecurseDepth)
        {
            _samplesPerPixel = samplesPerPixel;
            _maxRecurseDepth = maxRecurseDepth;
        }

        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            _pixelsRendered = 0;
            _pixelsInRender = frameBuffer.PixelCount;

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

                    Interlocked.Increment(ref _pixelsRendered);
                }

                WriteProgressToConsole();
            }
        }

        private void WriteProgressToConsole()
        {
            Console.CursorLeft = 0;
            Console.Write($"Render is {_pixelsRendered / (1.0f * _pixelsInRender):P}% complete");
        }

        private float NormalizeColor(float value, int numberOfSamples)
        {
            return MathUtil.Clamp((float)Math.Sqrt(value / numberOfSamples), 0.0f, 0.999f);
        }
    }
}
