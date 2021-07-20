﻿using System.Drawing;
using System.Windows.Forms;
using InAWeekend.Rendering;

namespace InAWeekend.Gui
{
    internal static class Gui
    {
        public static void RenderToWindow(this FrameBuffer frameBuffer)
        {
            Application.EnableVisualStyles();
            var form = new RenderForm();
            var image = frameBuffer.RenderToBitmap();
            form.DisplayImage(image);

            form.ShowDialog();
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
