using InAWeekend.Model;

namespace InAWeekend.Rendering
{
    class FrameBuffer
    {
        public int Width { get; }
        public int Height { get; }

        private readonly Color3[] _buffer;

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            _buffer = new Color3[width * height];
        }

        public Color3 this[int w, int h]
        {
            get => _buffer[w + Width * h];
            set => _buffer[w + Width * h] = value;
        }
    }
}