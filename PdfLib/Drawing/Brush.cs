using PdfSharp.Drawing;

namespace PdfLib.Drawing;

public abstract class Brush(XBrush brush)
{
    private readonly XBrush _brush = brush;

    public static implicit operator XBrush(Brush brush) => brush._brush;
}