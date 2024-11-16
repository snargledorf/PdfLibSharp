using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal interface ILayoutBuilder
{
    Size OuterSize { get; }
    Size ContentSize { get; }
    IElement Element { get; }
    ILayout BuildLayout(Rectangle bounds);
}