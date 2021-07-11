using InAWeekend.Geometry;
using InAWeekend.Model;
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
            var samplesPerPixel = 100;
            var maxRecurseDepth = 5;

            var camera = new Camera(2.0f, aspectRatio, 1.0f);
            var imageBuffer = new FrameBuffer(imageWidth, imageHeight);

            var scene = new Scene();
            scene.Add(new Sphere(new Point3(0, 0, -1), 0.5f));
            scene.Add(new Sphere(new Point3(0, -100.5f, -1), 100));

            var renderer = new RayTraceRenderer(samplesPerPixel, maxRecurseDepth);
            renderer.Render(scene, camera, imageBuffer);

            imageBuffer.SaveAsPpm();
        }
    }
}
