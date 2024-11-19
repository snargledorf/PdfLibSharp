using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Elements;

internal class LineElement(Pen pen) : BaseElement, ILineElement
{
    public Pen Pen { get; } = pen;
    public Direction Direction { get; set; }
}