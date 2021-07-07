using System;
using System.IO;

namespace InAWeekend.Rendering
{
    class ImageFileRenderer : IRenderer
    {
        private readonly string _filePath;

        public ImageFileRenderer(string filePath)
        {
            _filePath = filePath;
        }

        public void Render()
        {
            const int imageWidth = 256;
            const int imageHeight = 256;

            var outputPath = Path.Combine(_filePath, $"{DateTime.Now:yyyyMMdd-HHmmss}.ppm");
            using var imageFile = new StreamWriter(outputPath);
            
            imageFile.Write("P3\n");
            imageFile.WriteLine($"{imageWidth} {imageHeight}\n255\n");

            for (var j = imageHeight - 1; j >= 0; --j)
            {
                for (var i = 0; i < imageWidth; ++i)
                {
                    var r = (double)i / (imageWidth - 1);
                    var g = (double)j / (imageHeight - 1);
                    var b = 0.25;

                    var ir = (int)(255.999 * r);
                    var ig = (int)(255.999 * g);
                    var ib = (int)(255.999 * b);

                    imageFile.Write($"{ir} {ig} {ib}\n");
                }
            }
        }
    }
}
