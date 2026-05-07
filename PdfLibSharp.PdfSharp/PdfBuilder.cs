using System.IO;
using PdfLibSharp;
using PdfLibSharp.Drawing.Units;

public sealed class PdfBuilder : IPdfBuilder
{
    private PdfBuilder()
    {
    }
    
    public static PdfBuilder Instance { get; } = new();
    
    public IPdfDocument CreatePdfDocument(Stream stream, int ppi = UnitPixel.DefaultPixelsPerInch)
    {
        return new PdfSharpDocument(stream);
    }
}