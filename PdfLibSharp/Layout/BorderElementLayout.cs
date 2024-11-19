using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BorderElementLayout(Point point, Size contentSize, Margins margins, Pen? borderPen)
    : ElementLayout(point, contentSize, margins), IBorderLayout
{
    public Pen? BorderPen { get; } = borderPen;
}