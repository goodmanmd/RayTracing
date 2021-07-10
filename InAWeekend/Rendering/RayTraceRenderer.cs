using InAWeekend.Geometry;

namespace InAWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        public void Render(Scene scene, Camera camera, FrameBuffer frameBuffer)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    var u = (float)i / (imageWidth - 1);
                    var v = (float)j / (imageHeight - 1);

                    var ray = camera.GetRay(u, v);

                    var pixelColor = ray.Trace(scene);
                    frameBuffer[i, j] += pixelColor;
                }
            }
        }
    }
}
