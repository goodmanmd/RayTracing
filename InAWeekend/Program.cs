using InAWeekend.Geometry;
using InAWeekend.Model;
using InAWeekend.Model.Materials;
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

            var groundMaterial = new Lambertian(new Color3(0.8f, 0.8f, 0.0f));
            var centerMaterial = new Lambertian(new Color3(0.7f, 0.3f, 0.3f));
            var leftMaterial = new Metal(new Color3(0.8f, 0.8f, 0.8f));
            var rightMaterial = new Metal(new Color3(0.8f, 0.6f, 0.2f));

            var scene = new Scene();
            scene.Add(new Sphere(new Point3(0.0f, -100.5f, -1.0f), 100, groundMaterial));
            scene.Add(new Sphere(new Point3(0.0f, 0.0f, -1.0f), 0.5f, centerMaterial));
            scene.Add(new Sphere(new Point3(-1.0f, 0.0f, -1.0f), 0.5f, leftMaterial));
            scene.Add(new Sphere(new Point3(1.0f, 0.0f, -1.0f), 0.5f, rightMaterial));

            var renderer = new RayTraceRenderer(samplesPerPixel, maxRecurseDepth);
            renderer.Render(scene, camera, imageBuffer);

            imageBuffer.SaveAsPpm();
        }
    }
}
