using PdfLibSharp.Drawing;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public class PdfSharpImage(XImage xImage) : IImage
{
    private readonly XImage _xImage = xImage;
    public Size Size => _xImage.Size.ToSize();

    public static implicit operator XImage(PdfSharpImage pdfSharpImage) => pdfSharpImage._xImage;
}