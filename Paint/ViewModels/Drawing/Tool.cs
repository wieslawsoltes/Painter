using Avalonia;

namespace Paint.ViewModels.Drawing
{
    public abstract class Tool
    {
        public abstract void Pressed(IPainter painter, Point point);
        public abstract void Released(IPainter painter, Point point);
        public abstract void Moved(IPainter painter, Point point);
    }
}