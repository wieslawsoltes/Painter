using Avalonia;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class LineTool : Tool
    {
        private LineDrawOperation? _drawOperation;
        private bool _pressed;

        public override void Pressed(IPainter painter, Point point)
        {
            var p0 = new SKPoint((float)point.X, (float)point.Y);
            var p1 = new SKPoint((float)point.X, (float)point.Y);

            var paint = new SKPaint()
            {
                IsAntialias = false,
                FilterQuality = SKFilterQuality.High,
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
                StrokeWidth = 10
            };

            _drawOperation = new LineDrawOperation(p0, p1, paint);

            painter.AddOperation(_drawOperation);
            painter.InvalidatePainter();

            _pressed = true;
        }

        public override void Released(IPainter painter, Point point)
        {
            if (_pressed && _drawOperation is { })
            {
                painter.InvalidatePainter();
                _drawOperation.P1 = new SKPoint((float) point.X, (float) point.Y);
                _drawOperation = null;
                painter.InvalidatePainter();
            }

            _pressed = false;
        }

        public override void Moved(IPainter painter, Point point)
        {
            if (_pressed && _drawOperation is { })
            {
                _drawOperation.P1 = new SKPoint((float) point.X, (float) point.Y);
                painter.InvalidatePainter();
            }
        }
    }
}