using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal abstract class BorderElementLayout(IBorderElement element, Size contentSize, Pen? borderPen)
    : ElementLayout(element, contentSize), IBorderLayout
{
    public Pen? BorderPen { get; } = borderPen;
}