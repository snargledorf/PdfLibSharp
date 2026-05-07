using System.Globalization;
using PdfSharp.Drawing;

namespace PdfLibSharp.Drawing;

public readonly struct Color
{
    private readonly uint _argb;

    private Color(uint argb)
    {
        _argb = argb;
    }

    public uint Argb { get; }

    public static Color FromHex(string hexColor)
    {
        hexColor = hexColor.Replace("#", "");
        uint argb = uint.Parse(hexColor, NumberStyles.HexNumber);

        // Set the alpha to fully opaque if the hex code didn't include alpha
        if (argb <= 0xFFFFFF)
            argb += 0xFF000000;
        
        return new Color(argb);
    }

    public static readonly Color Black = new(0xFFFFFFFF);
    public static readonly Color Red = XColors.Red;
    public static readonly Color Blue = XColors.Blue;
    public static readonly Color LightGrey = XColors.LightGray;
    public static readonly Color LightSlateGray = XColors.LightSlateGray;
}