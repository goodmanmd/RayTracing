using System;
using InAWeekend.Geometry;
using InAWeekend.Gui;
using InAWeekend.Model;
using InAWeekend.Model.Materials;
using InAWeekend.Rendering;

namespace InAWeekend
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var aspectRatio = 16.0f / 9.0f;
            var imageHeight = 400;
            var imageWidth = (int)(aspectRatio * imageHeight);
            var samplesPerPixel = 100;
            var maxRecurseDepth = 5;
            var maxThreads = Environment.ProcessorCount;
            var outputToWindow = true;
            var outputToFile = true;

            var camera = new Camera(2.0f, aspectRatio, 1.0f);
            var imageBuffer = new FrameBuffer(imageWidth, imageHeight);

            var groundMaterial = new Lambertian(new Color3(0.8f, 0.8f, 0.0f));
            var centerMaterial = new Lambertian(new Color3(0.1f, 0.2f, 0.5f));
            var leftMaterial = new Dielectric(1.5f);
            var rightMaterial = new Metal(new Color3(0.8f, 0.6f, 0.2f), 0.0f);

            var scene = new Scene();
            scene.Add(new Sphere(new Point3(0.0f, -100.5f, -1.0f), 100, groundMaterial));
            scene.Add(new Sphere(new Point3(0.0f, 0.0f, -1.0f), 0.5f, centerMaterial));
            scene.Add(new Sphere(new Point3(-1.0f, 0.0f, -1.0f), 0.5f, leftMaterial));
            scene.Add(new Sphere(new Point3(-1.0f, 0.0f, -1.0f), -0.4f, leftMaterial));
            scene.Add(new Sphere(new Point3(1.0f, 0.0f, -1.0f), 0.5f, rightMaterial));

            var renderer = new RayTraceRenderer(samplesPerPixel, maxRecurseDepth, maxThreads);
            renderer.Render(scene, camera, imageBuffer);

            if (outputToWindow) imageBuffer.RenderToWindow();
            if (outputToFile) imageBuffer.SaveAsPpm();
        }
    }
}
