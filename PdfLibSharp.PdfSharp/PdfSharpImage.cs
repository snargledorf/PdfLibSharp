using PdfLibSharp.Drawing;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

internal class PdfSharpImage(XImage xImage) : Image(xImage.Size.ToSize()), IPdfSharpImage
{
    public XImage Image => xImage;
}