using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayoutFactory
{
    IElement Element { get; }
    ILayout CreateLayout(Size constraints);
}