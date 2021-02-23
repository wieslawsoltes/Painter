namespace Paint.ViewModels.Drawing
{
    public interface IPainter
    {
        void AddOperation(DrawOperation drawOperation);
        void RemoveOperation(DrawOperation drawOperation);
        void InvalidatePainter();
    }
}