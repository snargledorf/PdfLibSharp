using PdfLib.Drawing;
using PdfLib.Elements.Layout;

namespace PdfLib.Elements;

internal class LineElement(Pen pen) : BaseElement, ILineElement
{
    public Pen Pen { get; } = pen;
    public Direction Direction { get; set; }
}