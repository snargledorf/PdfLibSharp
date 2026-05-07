using System.Globalization;
namespace PdfLibSharp.Drawing;

public readonly record struct Color(uint Argb)
{
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