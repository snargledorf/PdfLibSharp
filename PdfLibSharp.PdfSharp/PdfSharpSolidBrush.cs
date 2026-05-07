using PdfLibSharp.Drawing;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

internal class PdfSharpSolidBrush(Color color) : SolidBrush(color), IPdfSharpBrush
{
    public XBrush XBrush { get; } = new XSolidBrush(color.ToXColor());
}