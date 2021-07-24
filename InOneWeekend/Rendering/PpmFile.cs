using System;
using System.IO;
using InOneWeekend.Util;

namespace InOneWeekend.Rendering
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
                    
                    var ir = MathUtil.Clamp((int)(256 * color.R), 0, 255);
                    var ig = MathUtil.Clamp((int)(256 * color.G), 0, 255);
                    var ib = MathUtil.Clamp((int)(256 * color.B), 0, 255);

                    imageFile.Write($"{ir} {ig} {ib}\n");
                }
            }
        }
    }
}