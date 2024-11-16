using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal abstract class ElementLayout(Point point, Size contentSize, Margins margins, Pen? borderPen) : ILayout
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
    
    public Pen? BorderPen { get; } = borderPen;
}