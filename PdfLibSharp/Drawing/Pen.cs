namespace PdfLibSharp.Drawing;

public sealed record Pen(Color Color, Dimension Width)
{
    public Pen(Color Color) : this(Color, 1)
    {
    }
}