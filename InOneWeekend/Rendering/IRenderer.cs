using InOneWeekend.Model;

namespace InOneWeekend.Rendering
{
    interface IRenderer
    {
        void Render(Scene scene, Camera camera, FrameBuffer frameBuffer);
    }
}
