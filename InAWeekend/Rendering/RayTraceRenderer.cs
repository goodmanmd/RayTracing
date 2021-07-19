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
        private readonly int _maxThreads;

        private int _pixelsRendered = 0;
        private int _pixelsInRender = 0;

        public RayTraceRenderer(int samplesPerPixel, int maxRecurseDepth, int maxThreads)
        {
            _samplesPerPixel = samplesPerPixel;
            _maxRecurseDepth = maxRecurseDepth;
            _maxThreads = maxThreads <= 1 ? 1 : maxThreads;
        }

        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer)
        {
            _pixelsRendered = 0;
            _pixelsInRender = frameBuffer.PixelCount;

            if (_maxThreads == 1)
            {
                //if we've only got 1 thread, run the render directly on the current thread
                RenderCore(scene, camera, frameBuffer, 0);
            }

            else
            {
                //otherwise, spin up multiple threads and divide/conquer the render
                var tasks = new Task[_maxThreads];

                for (var i = 0; i < _maxThreads; ++i)
                {
                    var threadId = i;
                    tasks[threadId] = new Task(() => RenderCore(scene, camera, frameBuffer, threadId));
                    tasks[threadId].Start();
                }

                Task.WaitAll(tasks);
            }
        }

        private void RenderCore(Scene scene, Camera camera, FrameBuffer frameBuffer, int threadId)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            for (var j = imageHeight - threadId - 1; j >= 0; j -= _maxThreads)
            {
                WriteProgressToConsole();

                for (var i = 0; i < imageWidth; ++i)
                {
                    float r = 0, g = 0, b = 0;

                    for (var sample = 0; sample < _samplesPerPixel; sample++)
                    {
                        var u = (i + ThreadLocalRandom.Instance.NextFloat()) / (imageWidth - 1);
                        var v = (j + ThreadLocalRandom.Instance.NextFloat()) / (imageHeight - 1);

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
            }

            WriteProgressToConsole();
        }

        private readonly object _consoleLock = new object();
        private void WriteProgressToConsole()
        {
            lock (_consoleLock)
            {
                Console.CursorLeft = 0;
                Console.Write($"Render is {_pixelsRendered / (1.0f * _pixelsInRender):P}% complete");
            }
        }

        private float NormalizeColor(float value, int numberOfSamples)
        {
            return MathUtil.Clamp((float)Math.Sqrt(value / numberOfSamples), 0.0f, 0.999f);
        }
    }
}
