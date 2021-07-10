using InAWeekend.Geometry;

namespace InAWeekend.Rendering
{
    class RayTraceRenderer : IRenderer
    {
        public void Render(Camera camera, FrameBuffer frameBuffer)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    var u = (float)i / (imageWidth - 1);
                    var v = (float)j / (imageHeight - 1);

                    var ray = new Ray
                    (
                        camera.Origin,
                        camera.LowerLeftCorner.AsVector() + u * camera.Horizontal + v * camera.Vertical - camera.Origin.AsVector()
                    );

                    var pixelColor = ray.NormalColor();
                    frameBuffer[i, j] += pixelColor;
                }
            }
        }
    }
}
