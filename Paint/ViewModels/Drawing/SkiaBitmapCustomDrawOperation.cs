using Avalonia;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class SkiaBitmapCustomDrawOperation : ICustomDrawOperation
    {
        private readonly SKBitmap _bitmap;

        public SkiaBitmapCustomDrawOperation(SKBitmap bitmap, Rect bounds)
        {
            _bitmap = bitmap;
            Bounds = bounds;
        }

        public void Dispose()
        {
        }

        public bool HitTest(Point p) => false;

        public void Render(IDrawingContextImpl context)
        {
            if (context is not ISkiaDrawingContextImpl skiaDrawingContextImpl)
            {
                return;
            }

            if (skiaDrawingContextImpl.SkCanvas is { } canvas)
            {
                canvas.DrawBitmap(_bitmap, SKPoint.Empty);
            }
        }

        public Rect Bounds { get; }

        public bool Equals(ICustomDrawOperation? other) => false;
    }
}