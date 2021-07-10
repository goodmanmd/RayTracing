using InAWeekend.Rendering;

namespace InAWeekend
{
    class Program
    {
        static void Main(string[] args)
        {
            var aspectRatio = 16.0f / 9.0f;
            var imageHeight = 400;
            var imageWidth = (int)(aspectRatio * imageHeight);

            var camera = new Camera(2.0f, aspectRatio, 1.0f);
            var imageBuffer = new FrameBuffer(imageWidth, imageHeight);

            var renderer = new RayTraceRenderer();
            renderer.Render(camera, imageBuffer);

            imageBuffer.SaveAsPpm();
        }
    }
}
