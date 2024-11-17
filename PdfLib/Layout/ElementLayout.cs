using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal abstract class ElementLayout(Point point, Size contentSize, Margins margins) : ILayout
{
    public Rectangle OuterBounds { get; } = new(
        Point: point,
        Size: contentSize + new Size(margins.Left + margins.Right, margins.Top + margins.Bottom)
    );

    public Rectangle ContentBounds { get; } = new
    (
        Point: point + new Point(margins.Left, margins.Top),
        Size: contentSize
    );
}