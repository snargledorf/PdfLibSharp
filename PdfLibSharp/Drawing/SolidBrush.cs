namespace PdfLibSharp.Drawing;

public class SolidBrush(Color color) : Brush
{
    public Color Color { get; } = color;
}