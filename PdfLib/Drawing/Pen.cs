using PdfSharp.Drawing;

namespace PdfLibrary.Drawing;

public sealed record Pen(Color Color, Dimension Width)
{
    public static implicit operator XPen(Pen pen) => new(pen.Color, pen.Width);

    public Pen(Color Color) : this(Color, 1)
    {
    }
}