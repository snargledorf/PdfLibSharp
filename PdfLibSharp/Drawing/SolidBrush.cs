namespace PdfLibSharp.Drawing;

public abstract class SolidBrush(Color color) : Brush
{
    public Color Color { get; } = color;
}