using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayout
{
    Margins Margins { get; }
    Size ContentSize { get; }

    PositionedLayout ToPositionedLayout(Rectangle outerBounds);
}