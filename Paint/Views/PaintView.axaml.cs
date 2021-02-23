using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Paint.ViewModels.Drawing;

namespace Paint.Views
{
    public class PaintView : UserControl
    {
        private readonly Painter _painter;

        public PaintView()
        {
            InitializeComponent();

            _painter = new Painter(400, 400);
            _painter.Invalidate += (_, _) => this.InvalidateVisual();

            AddHandler(PointerPressedEvent, PointerPressedHandler, RoutingStrategies.Tunnel);
            AddHandler(PointerReleasedEvent, PointerReleaseHandler, RoutingStrategies.Tunnel);
            AddHandler(PointerMovedEvent, PointerMovedHandler, RoutingStrategies.Tunnel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void PointerPressedHandler(object? sender, PointerPressedEventArgs e)
        {
            _painter.Pressed(e.GetPosition(this));
        }

        private void PointerReleaseHandler(object? sender, PointerReleasedEventArgs e)
        {
            _painter.Released(e.GetPosition(this));
        }

        private void PointerMovedHandler(object? sender, PointerEventArgs e)
        {
            _painter.Moved(e.GetPosition(this));
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // context.DrawRectangle(null, new Pen(Brushes.Black, 2), new Rect(0, 0, Bounds.Width, Bounds.Height));
  
            _painter.Render(context);
        }
    }
}