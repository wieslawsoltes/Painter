using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class LineDrawOperation : DrawOperation
    {
        public SKPoint P0 { get; set; }
        public SKPoint P1 { get; set; }
        public SKPaint Paint { get; set; }

        public LineDrawOperation(SKPoint p0, SKPoint p1, SKPaint paint)
        {
            P0 = p0;
            P1 = p1;
            Paint = paint;
        }

        public override void Render(SKCanvas canvas)
        {
            canvas.DrawLine(P0, P1, Paint);
        }
    }
}