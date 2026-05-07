using PdfLibSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

internal sealed class PdfSharpBrushLoader : IBrushLoader
{
    private PdfSharpBrushLoader()
    {
    }

    public SolidBrush LoadSolidBrush(Color color)
    {
        return new PdfSharpSolidBrush(color);
    }

    public static PdfSharpBrushLoader Instance { get; } = new();
}