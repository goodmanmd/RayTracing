using System.ComponentModel;
using System.Windows.Forms;
using InAWeekend.Rendering;

namespace InAWeekend.Gui
{
    partial class RenderForm : Form
    {
        private readonly FrameBuffer _frameBuffer;
        private readonly Timer _redrawTimer;

        public bool EnableRefresh
        {
            get => _redrawTimer.Enabled;
            set
            {
                _redrawTimer.Enabled = value;

                //refresh one final time when timer is disabled
                //in case the buffer is updated but the timer hasn't fired
                if (!value) RefreshImage();
            }
        }

        public RenderForm(FrameBuffer frameBuffer)
        {
            _frameBuffer = frameBuffer;
            InitializeComponent();
            Height = _frameBuffer.Height;
            Width = _frameBuffer.Width;

            RefreshImage();

            _redrawTimer = new Timer
            {
                Enabled = false,
                Interval = 1000
            };

            _redrawTimer.Tick += (sender, args) => RefreshImage();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _redrawTimer.Stop();
            base.OnClosing(e);
        }

        private void RefreshImage()
        {
            PictureBox.Image = _frameBuffer.RenderToBitmap();
        }
    }
}
