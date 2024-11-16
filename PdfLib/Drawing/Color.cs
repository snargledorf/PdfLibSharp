using PdfSharp.Drawing;

namespace PdfLib.Drawing;

public readonly struct Color
{
    private readonly XColor _color;

    private Color(XColor color)
    {
        _color = color;
    }

    public static implicit operator XColor(Color color) => color._color;

    public static implicit operator Color(XColor color) => new(color);

    public static readonly Color Black = XColors.Black;
    public static readonly Color Red = XColors.Red;
    public static readonly Color Blue = XColors.Blue;
}