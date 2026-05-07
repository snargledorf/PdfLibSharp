using PdfLibSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public sealed class PdfSharpDrawingLoaders : IDrawingLoaders
{
    private PdfSharpDrawingLoaders()
    {
    }
    
    public static PdfSharpDrawingLoaders Instance { get; } = new();

    public IImageLoader ImageLoader { get; } = PdfSharpImageLoader.Instance;
}