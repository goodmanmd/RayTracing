using System;
using System.IO;

namespace InAWeekend.Rendering
{
    internal static class PpmFile
    {
        public static void SaveAsPpm(this FrameBuffer frameBuffer)
        {
            var outputPath = Path.Combine(".\\", $"{DateTime.Now:yyyyMMdd-HHmmss}.ppm");
            frameBuffer.SaveAsPpm(outputPath);
        }

        public static void SaveAsPpm(this FrameBuffer frameBuffer, string outputPath)
        {
            var imageHeight = frameBuffer.Height;
            var imageWidth = frameBuffer.Width;

            using var imageFile = new StreamWriter(outputPath);

            imageFile.Write("P3\n");
            imageFile.WriteLine($"{imageWidth} {imageHeight}\n255\n");

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    var color = frameBuffer[i,j];
                    
                    var ir = (int)(255.999 * color.R);
                    var ig = (int)(255.999 * color.G);
                    var ib = (int)(255.999 * color.B);

                    imageFile.Write($"{ir} {ig} {ib}\n");
                }
            }
        }
    }
}