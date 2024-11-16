using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal interface ILayoutBuilder
{
    Size OuterSize { get; }
    Size ContentSize { get; }
    IElement Element { get; }
    ILayout BuildLayout(Rectangle bounds);
}