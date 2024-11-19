using System.Globalization;
using PdfSharp.Drawing;

namespace PdfLibSharp.Drawing;

public readonly struct Color
{
    private readonly XColor _color;

    private Color(XColor color)
    {
        _color = color;
    }

    public static Color FromHex(string hexColor)
    {
        hexColor = hexColor.Replace("#", "");
        uint argb = uint.Parse(hexColor, NumberStyles.HexNumber);

        // Set the alpha to fully opaque if the hex code didn't include alpha
        if (argb <= 0xFFFFFF)
            argb += 0xFF000000;
        
        return XColor.FromArgb(argb);
    }

    public static implicit operator XColor(Color color) => color._color;

    public static implicit operator Color(XColor color) => new(color);

    public static readonly Color Black = XColors.Black;
    public static readonly Color Red = XColors.Red;
    public static readonly Color Blue = XColors.Blue;
    public static readonly Color LightGrey = XColors.LightGray;
    public static readonly Color LightSlateGray = XColors.LightSlateGray;
}