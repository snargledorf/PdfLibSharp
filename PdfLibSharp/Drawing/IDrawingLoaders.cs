namespace PdfLibSharp.Drawing;

public interface IDrawingLoaders
{
    IImageLoader ImageLoader { get; }
    IBrushLoader BrushLoader { get; }
}