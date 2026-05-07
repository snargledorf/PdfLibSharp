using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

internal interface IPdfSharpImage
{
    XImage Image { get; }
}