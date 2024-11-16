using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal interface ILayout
{
    Rectangle OuterBounds { get; }
    Rectangle ContentBounds { get; }
    Pen? BorderPen { get; }
}