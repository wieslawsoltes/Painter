using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public abstract class DrawOperation
    {
        public abstract void Render(SKCanvas canvas);
    }
}