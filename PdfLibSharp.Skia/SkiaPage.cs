using PdfLibSharp.Drawing;
using SkiaSharp;

namespace PdfLibSharp.Skia;

public class SkiaPage(Size size, SKDocument skDocument, int ppi) : IPdfPage
{
    private SKCanvas PageCanvas => field ??= skDocument.BeginPage((float)size.Width.Points, (float)size.Height.Points);
    
    public IGraphics CreateGraphics()
    {
        return new Graphics(PageCanvas, ppi);
    }

    public IMeasureGraphics CreateMeasureGraphics(Size size)
    {
        return new Graphics(PageCanvas, ppi);
    }

    public void Dispose()
    {
        skDocument.EndPage();
    }
}