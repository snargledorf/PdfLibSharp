using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayoutBuilder
{
    Size OuterSize { get; }
    Size ContentSize { get; }
    IElement Element { get; }
    ILayout BuildLayout(Rectangle bounds);
}