using PdfLib.Drawing;

namespace PdfLib.Elements.Layout;

internal class StackContainer(Direction direction) : Container, IStackContainer
{
    public Direction Direction { get; set; } = direction;
    
    public Dimension Gap { get; set; }
}