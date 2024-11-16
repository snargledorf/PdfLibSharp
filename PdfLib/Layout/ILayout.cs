using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal interface ILayout
{
    Rectangle OuterBounds { get; }
    Rectangle ContentBounds { get; }
    Pen? BorderPen { get; }
}