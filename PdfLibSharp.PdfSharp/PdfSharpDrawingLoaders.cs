using PdfLibSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public sealed class PdfSharpDrawingLoaders : IDrawingLoaders
{
    public IImageLoader ImageLoader { get; } = PdfSharpImageLoader.Instance;
    public IBrushLoader BrushLoader { get; } = PdfSharpBrushLoader.Instance;
}