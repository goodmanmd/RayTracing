using InAWeekend.Geometry;

namespace InAWeekend.Rendering
{
    interface IRenderer
    {
        void Render(Scene scene, Camera camera, FrameBuffer frameBuffer);
    }
}
