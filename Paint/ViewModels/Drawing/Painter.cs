using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class Painter : IPainter
    {
        private int _width;
        private int _height;
        private SKBitmap _original;
        private SKBitmap _working;
        private List<DrawOperation> _drawOperations;
        private Tool? _currentTool;

        public event EventHandler<EventArgs>? Invalidate;
        
        public Painter(int width, int height)
        {
            _width = width;
            _height = height;

            // SKImageInfo.PlatformColorType
            // SKColorType.Rgba8888
            // SKColorType.Bgra8888

            _original = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul); 
            _working = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul);

            _drawOperations = new List<DrawOperation>();
            _currentTool = new LineTool();

            ClearOriginal();
        }

        protected virtual void OnInvalidate(EventArgs e) => Invalidate?.Invoke(this, e);

        public void AddOperation(DrawOperation drawOperation)
        {
            _drawOperations.Add(drawOperation);
        }

        public void RemoveOperation(DrawOperation drawOperation)
        {
            _drawOperations.Remove(drawOperation);
        }

        public void InvalidatePainter()
        {
            OnInvalidate(new EventArgs());
        }

        public void Pressed(Point point)
        {
            _currentTool?.Pressed(this, point);
        }

        public void Released(Point point)
        {
            _currentTool?.Released(this, point);
        }

        public void Moved(Point point)
        {
            _currentTool?.Moved(this, point);
        }
        
        private void ClearOriginal()
        {
            using var canvas = new SKCanvas(_original);

            canvas.Clear(SKColors.White);
        }

        private void BlitWorking()
        {
            using var canvas = new SKCanvas(_working);

            canvas.Clear(SKColors.Transparent);

            canvas.DrawBitmap(_original, SKPoint.Empty);

            foreach (var operation in _drawOperations)
            {
                operation.Render(canvas);
            }
        }

        public void Render(DrawingContext context)
        {
            BlitWorking();

            var bounds = new Rect(0, 0, _width, _height);
            var custom = new SkiaBitmapCustomDrawOperation(_working, bounds);
            context.Custom(custom);
        }
    }
}