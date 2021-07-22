using System;
using System.Diagnostics;
using System.Numerics;
using InAWeekend.Geometry;
using InAWeekend.Gui;
using InAWeekend.Model;
using InAWeekend.Model.Materials;
using InAWeekend.Rendering;
using InAWeekend.Util;

namespace InAWeekend
{
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var aspectRatio = 3.0f / 2.0f;
            var imageWidth = 1200;
            var imageHeight = (int)(imageWidth / aspectRatio);
            var samplesPerPixel = 10;
            var maxRecurseDepth = 50;
            var maxThreads = Debugger.IsAttached
                                    ? 1 
                                    : Environment.ProcessorCount;
            var outputToWindow = true;
            var outputToFile = true;

            var lookFrom = new Point3(13, 2, 3);
            var lookAt = new Point3(0, 0, 0);
            var up = Vector3.UnitY;
            var verticalFieldOfViewInDegrees = 20;
            var aperture = 0.1f;
            var focusDistance = 10.0f;

            var camera = new Camera(lookFrom, lookAt, up, verticalFieldOfViewInDegrees, aspectRatio, aperture, focusDistance);
            var imageBuffer = new FrameBuffer(imageWidth, imageHeight);

            var scene = RandomScene();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var renderer = new RayTraceRenderer(samplesPerPixel, maxRecurseDepth, maxThreads);
            renderer.Render(scene, camera, imageBuffer);

            stopWatch.Stop();
            Console.WriteLine();
            Console.WriteLine($"Render complete in {stopWatch.Elapsed:g}");
            Console.WriteLine($"Total paths: {renderer.TotalPaths:N0}");
            Console.WriteLine($"Paths per second: {renderer.TotalPaths / stopWatch.Elapsed.TotalSeconds:N} at max depth of {maxRecurseDepth}");

            if (outputToFile) imageBuffer.SaveAsPpm();
            if (outputToWindow) imageBuffer.RenderToWindow();
        }

        private static Scene ThreeBallScene()
        {
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

            return scene;
        }

        private static Scene RandomScene()
        {
            var scene = new Scene();

            var groundMaterial = new Lambertian(new Color3(0.5f, 0.5f, 0.5f));
            scene.Add(new Sphere(new Point3(0, -1000, 0), 1000, groundMaterial));

            for (var a = -11; a < 11; a++)
            {
                for (var b = -11; b < 11; b++)
                {
                    var chooseMat = ThreadLocalRandom.Instance.NextFloat();
                    var center = new Point3(a +0.9f * ThreadLocalRandom.Instance.NextFloat(), 0.2f, b + 0.9f * ThreadLocalRandom.Instance.NextFloat());

                    if ((center - new Point3(4, 0.2f, 0)).AsVector().Length() > 0.9)
                    {
                        IMaterial sphereMaterial;

                        if (chooseMat < 0.8)
                        {
                            // diffuse
                            var albedo = Color3.Random(ThreadLocalRandom.Instance) * Color3.Random(ThreadLocalRandom.Instance);
                            sphereMaterial = new Lambertian(albedo);
                            scene.Add(new Sphere(center, 0.2f, sphereMaterial));
                        }
                        else if (chooseMat < 0.95)
                        {
                            // metal
                            var albedo = Color3.Random(ThreadLocalRandom.Instance, 0.5f, 1.0f);
                            var fuzz = ThreadLocalRandom.Instance.NextFloat(0, 0.5f);
                            sphereMaterial = new Metal(albedo, fuzz);
                            scene.Add(new Sphere(center, 0.2f, sphereMaterial));
                        }
                        else
                        {
                            // glass
                            sphereMaterial = new Dielectric(1.5f);
                            scene.Add(new Sphere(center, 0.2f, sphereMaterial));
                        }
                    }
                }
            }

            var material1 = new Dielectric(1.5f);
            scene.Add(new Sphere(new Point3(0, 1, 0), 1.0f, material1));

            var material2 = new Lambertian(new Color3(0.4f, 0.2f, 0.1f));
            scene.Add(new Sphere(new Point3(-4, 1, 0), 1.0f, material2));

            var material3 = new Metal(new Color3(0.7f, 0.6f, 0.5f), 0.0f);
            scene.Add(new Sphere(new Point3(4, 1, 0), 1.0f, material3));

            return scene;
        }
    }
}
