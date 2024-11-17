using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal abstract class BorderElementLayout(Point point, Size contentSize, Margins margins, Pen? borderPen)
    : ElementLayout(point, contentSize, margins), IBorderLayout
{
    public Pen? BorderPen { get; } = borderPen;
}