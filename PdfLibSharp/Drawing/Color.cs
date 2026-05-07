using System.Globalization;
namespace PdfLibSharp.Drawing;

public readonly struct Color
{
    private readonly uint _argb;

    internal Color(uint argb)
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
}