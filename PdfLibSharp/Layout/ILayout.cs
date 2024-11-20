using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal interface ILayout
{
    ElementSizing Sizing { get; }
    Margins Margins { get; }
    Size ContentSize { get; }

    PositionedLayout ToPositionedLayout(Rectangle outerBounds);
}