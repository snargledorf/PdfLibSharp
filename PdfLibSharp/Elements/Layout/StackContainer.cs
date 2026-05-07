using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements.Layout;

internal class StackContainer(Direction direction, Pdf pdf) : Container(pdf), IStackContainer
{
    public Direction Direction { get; set; } = direction;
    
    public Dimension Gap { get; set; }
}