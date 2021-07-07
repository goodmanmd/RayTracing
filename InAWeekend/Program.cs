using InAWeekend.Rendering;

namespace InAWeekend
{
    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new ImageFileRenderer(".\\");
            renderer.Render();
        }
    }
}
