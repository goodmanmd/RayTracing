using System;
using System.Diagnostics;
using System.Numerics;
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
            var maxThreads = Debugger.IsAttached
                                    ? 1 
                                    : Environment.ProcessorCount;
            var outputToWindow = true;
            var outputToFile = false;

            var lookFrom = new Point3(-2, 2, 1);
            var lookAt = new Point3(0, 0, -1);
            var up = Vector3.UnitY;
            var verticalFieldOfViewInDegrees = 20;
            var aperture = 2.0f;
            var focusDistance = (lookFrom - lookAt).AsVector().Length();

            var camera = new Camera(lookFrom, lookAt, up, verticalFieldOfViewInDegrees, aspectRatio, aperture, focusDistance);
            var imageBuffer = new FrameBuffer(imageWidth, imageHeight);

            var groundMaterial = new Lambertian(new Color3(0.8f, 0.8f, 0.0f));
            var centerMaterial = new Lambertian(new Color3(0.1f, 0.2f, 0.5f));
            var leftMaterial = new Dielectric(1.5f);
            var rightMaterial = new Metal(new Color3(0.8f, 0.6f, 0.2f), 0.0f);

            var scene = new Scene();
            scene.Add(new Sphere(new Point3(0.0f, -100.5f, -1.0f), 100, groundMaterial));
            scene.Add(new Sphere(new Point3(0.0f, 0.0f, -1.0f), 0.5f, centerMaterial));
            scene.Add(new Sphere(new Point3(-1.0f, 0.0f, -1.0f), 0.5f, leftMaterial));
            scene.Add(new Sphere(new Point3(-1.0f, 0.0f, -1.0f), -0.45f, leftMaterial));
            scene.Add(new Sphere(new Point3(1.0f, 0.0f, -1.0f), 0.5f, rightMaterial));

            var renderer = new RayTraceRenderer(samplesPerPixel, maxRecurseDepth, maxThreads);
            renderer.Render(scene, camera, imageBuffer);

            if (outputToWindow) imageBuffer.RenderToWindow();
            if (outputToFile) imageBuffer.SaveAsPpm();
        }
    }
}
