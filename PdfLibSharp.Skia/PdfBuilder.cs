using PdfLibSharp.Drawing.Units;
using SkiaSharp;

namespace PdfLibSharp.Skia;

public sealed class PdfBuilder : IPdfBuilder
{
    private PdfBuilder()
    {
    }
    
    public static PdfBuilder Instance { get; } = new();
    
    public IPdfDocument CreatePdfDocument(Stream stream, int ppi = UnitPixel.DefaultPixelsPerInch)
    {
        var skDocument = SKDocument.CreatePdf(stream, ppi);
        return new SkiaPdfDocument(skDocument, stream, ppi);
    }
}