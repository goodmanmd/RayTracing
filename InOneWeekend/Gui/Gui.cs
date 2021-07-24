using System.Drawing;
using System.Windows.Forms;
using InOneWeekend.Rendering;

namespace InOneWeekend.Gui
{
    internal static class Gui
    {
        public static RenderForm CreateUiWindow(this FrameBuffer frameBuffer)
        {
            Application.EnableVisualStyles();
            var form = new RenderForm(frameBuffer);

            return form;
        }

        public static Bitmap RenderToBitmap(this FrameBuffer frameBuffer)
        {
            var bitmap = new Bitmap(frameBuffer.Width, frameBuffer.Height);
            for (var i = 0; i < frameBuffer.Width; i++)
            {
                for (var j = 0; j < frameBuffer.Height; j++)
                {
                    bitmap.SetPixel(i, frameBuffer.Height - j - 1, frameBuffer[i, j]);
                }
            }

            return bitmap;
        }
    }
}
