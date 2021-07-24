using System;
using System.Threading;
using System.Threading.Tasks;
using InOneWeekend.Core;
using InOneWeekend.Model;
using InOneWeekend.Util;

namespace InOneWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        private readonly Options _options;

        private int _pixelsRendered = 0;
        private int _pixelsInRender = 0;
        private long _totalPaths = 0;

        public long TotalPaths => _totalPaths;

        public RayTraceRenderer(Options options)
        {
            _options = options;
        }

        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer)
        {
            _pixelsRendered = 0;
            _pixelsInRender = frameBuffer.PixelCount;

            if (_options.MaxThreads == 1)
            {
                //if we've only got 1 thread, run the render directly on the current thread
                RenderCore(scene, camera, frameBuffer, 0);
            }

            else
            {
                //otherwise, spin up multiple threads and divide/conquer the render
                var tasks = new Task[_options.MaxThreads];

                for (var i = 0; i < _options.MaxThreads; ++i)
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

            for (var j = imageHeight - threadId - 1; j >= 0; j -= _options.MaxThreads)
            {
                WriteProgressToConsole();

                for (var i = 0; i < imageWidth; ++i)
                {
                    float r = 0, g = 0, b = 0;

                    for (var sample = 0; sample < _options.SamplesPerPixel; sample++)
                    {
                        var u = (i + ThreadLocalRandom.Instance.NextFloat()) / (imageWidth - 1);
                        var v = (j + ThreadLocalRandom.Instance.NextFloat()) / (imageHeight - 1);

                        var ray = camera.GetRay(u, v);

                        var pixelColor = ray.Trace(scene, _options.MaxRecurseDepth);

                        r += pixelColor.R;
                        g += pixelColor.G;
                        b += pixelColor.B;
                    }

                    frameBuffer[i, j] = new Color3
                    (
                        NormalizeColor(r, _options.SamplesPerPixel),
                        NormalizeColor(g, _options.SamplesPerPixel),
                        NormalizeColor(b, _options.SamplesPerPixel)
                    );

                    Interlocked.Add(ref _totalPaths, _options.SamplesPerPixel);
                    Interlocked.Increment(ref _pixelsRendered);
                }
            }

            WriteProgressToConsole();
        }

        private readonly object _consoleLock = new object();
        private void WriteProgressToConsole()
        {
            if (!_options.ShowProgress) return;
            lock (_consoleLock)
            {
                Console.CursorLeft = 0;
                Console.Write($"Render is {_pixelsRendered / (1.0f * _pixelsInRender):P} complete");
            }
        }

        private float NormalizeColor(float value, int numberOfSamples)
        {
            return MathUtil.Clamp((float)Math.Sqrt(value / numberOfSamples), 0.0f, 0.999f);
        }
    }
}
