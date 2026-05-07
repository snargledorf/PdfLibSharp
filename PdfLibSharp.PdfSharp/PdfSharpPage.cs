using PdfLibSharp;
using PdfLibSharp.Drawing;
using PdfLibSharp.PdfSharp;
using PdfSharp.Pdf;

public class PdfSharpPage(PdfPage pdfPage) : IPdfPage
{
    public IGraphics CreateGraphics()
    {
        return Graphics.FromPdfPage(pdfPage);
    }

    public IMeasureGraphics CreateMeasureGraphics(Size size)
    {
        return Graphics.ForMeasure(size.ToXSize());
    }

    public void Dispose()
    {
        // NoOp
    }
}