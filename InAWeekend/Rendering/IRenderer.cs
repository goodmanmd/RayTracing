using InAWeekend.Model;

namespace InAWeekend.Rendering
{
    interface IRenderer
    {
        void Render(Scene scene, Camera camera, FrameBuffer frameBuffer);
    }
}
