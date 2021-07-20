using System.Drawing;
using System.Windows.Forms;

namespace InAWeekend.Gui
{
    public partial class RenderForm : Form
    {
        public RenderForm()
        {
            InitializeComponent();
        }

        public void DisplayImage(Bitmap bitmap)
        {
            PictureBox.Image = bitmap;
            Height = bitmap.Height;
            Width = bitmap.Width;
        }
    }
}
