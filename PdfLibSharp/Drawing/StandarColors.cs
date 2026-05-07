using System.Collections.Frozen;

namespace PdfLibSharp.Drawing;

public readonly record struct StandardColors(uint Argb)
{
    public Color Color { get; } = new(Argb);

    public static bool IsStandardColor(uint argb) => StandardColorArgbLookup.Contains(argb);

    public static implicit operator StandardColors(uint argb) => new(argb);
    public static implicit operator uint(StandardColors color) => color.Argb;
    public static implicit operator Color(StandardColors standardColor) => standardColor.Color;

    public static readonly StandardColors AliceBlue = 0xFFF0F8FF;
    public static readonly StandardColors AntiqueWhite = 0xFFFAEBD7;
    public static readonly StandardColors Aqua = 0xFF00FFFF;
    public static readonly StandardColors Aquamarine = 0xFF7FFFD4;
    public static readonly StandardColors Azure = 0xFFF0FFFF;
    public static readonly StandardColors Beige = 0xFFF5F5DC;
    public static readonly StandardColors Bisque = 0xFFFFE4C4;
    public static readonly StandardColors Black = 0xFF000000;
    public static readonly StandardColors BlanchedAlmond = 0xFFFFEBCD;
    public static readonly StandardColors Blue = 0xFF0000FF;
    public static readonly StandardColors BlueViolet = 0xFF8A2BE2;
    public static readonly StandardColors Brown = 0xFFA52A2A;
    public static readonly StandardColors BurlyWood = 0xFFDEB887;
    public static readonly StandardColors CadetBlue = 0xFF5F9EA0;
    public static readonly StandardColors Chartreuse = 0xFF7FFF00;
    public static readonly StandardColors Chocolate = 0xFFD2691E;
    public static readonly StandardColors Coral = 0xFFFF7F50;
    public static readonly StandardColors CornflowerBlue = 0xFF6495ED;
    public static readonly StandardColors Cornsilk = 0xFFFFF8DC;
    public static readonly StandardColors Crimson = 0xFFDC143C;
    public static readonly StandardColors Cyan = 0xFF00FFFF;
    public static readonly StandardColors DarkBlue = 0xFF00008B;
    public static readonly StandardColors DarkCyan = 0xFF008B8B;
    public static readonly StandardColors DarkGoldenrod = 0xFFB8860B;
    public static readonly StandardColors DarkGray = 0xFFA9A9A9;
    public static readonly StandardColors DarkGreen = 0xFF006400;
    public static readonly StandardColors DarkKhaki = 0xFFBDB76B;
    public static readonly StandardColors DarkMagenta = 0xFF8B008B;
    public static readonly StandardColors DarkOliveGreen = 0xFF556B2F;
    public static readonly StandardColors DarkOrange = 0xFFFF8C00;
    public static readonly StandardColors DarkOrchid = 0xFF9932CC;
    public static readonly StandardColors DarkRed = 0xFF8B0000;
    public static readonly StandardColors DarkSalmon = 0xFFE9967A;
    public static readonly StandardColors DarkSeaGreen = 0xFF8FBC8B;
    public static readonly StandardColors DarkSlateBlue = 0xFF483D8B;
    public static readonly StandardColors DarkSlateGray = 0xFF2F4F4F;
    public static readonly StandardColors DarkTurquoise = 0xFF00CED1;
    public static readonly StandardColors DarkViolet = 0xFF9400D3;
    public static readonly StandardColors DeepPink = 0xFFFF1493;
    public static readonly StandardColors DeepSkyBlue = 0xFF00BFFF;
    public static readonly StandardColors DimGray = 0xFF696969;
    public static readonly StandardColors DodgerBlue = 0xFF1E90FF;
    public static readonly StandardColors Firebrick = 0xFFB22222;
    public static readonly StandardColors FloralWhite = 0xFFFFFAF0;
    public static readonly StandardColors ForestGreen = 0xFF228B22;
    public static readonly StandardColors Fuchsia = 0xFFFF00FF;
    public static readonly StandardColors Gainsboro = 0xFFDCDCDC;
    public static readonly StandardColors GhostWhite = 0xFFF8F8FF;
    public static readonly StandardColors Gold = 0xFFFFD700;
    public static readonly StandardColors Goldenrod = 0xFFDAA520;
    public static readonly StandardColors Gray = 0xFF808080;
    public static readonly StandardColors Green = 0xFF008000;
    public static readonly StandardColors GreenYellow = 0xFFADFF2F;
    public static readonly StandardColors Honeydew = 0xFFF0FFF0;
    public static readonly StandardColors HotPink = 0xFFFF69B4;
    public static readonly StandardColors IndianRed = 0xFFCD5C5C;
    public static readonly StandardColors Indigo = 0xFF4B0082;
    public static readonly StandardColors Ivory = 0xFFFFFFF0;
    public static readonly StandardColors Khaki = 0xFFF0E68C;
    public static readonly StandardColors Lavender = 0xFFE6E6FA;
    public static readonly StandardColors LavenderBlush = 0xFFFFF0F5;
    public static readonly StandardColors LawnGreen = 0xFF7CFC00;
    public static readonly StandardColors LemonChiffon = 0xFFFFFACD;
    public static readonly StandardColors LightBlue = 0xFFADD8E6;
    public static readonly StandardColors LightCoral = 0xFFF08080;
    public static readonly StandardColors LightCyan = 0xFFE0FFFF;
    public static readonly StandardColors LightGoldenrodYellow = 0xFFFAFAD2;
    public static readonly StandardColors LightGray = 0xFFD3D3D3;
    public static readonly StandardColors LightGreen = 0xFF90EE90;
    public static readonly StandardColors LightPink = 0xFFFFB6C1;
    public static readonly StandardColors LightSalmon = 0xFFFFA07A;
    public static readonly StandardColors LightSeaGreen = 0xFF20B2AA;
    public static readonly StandardColors LightSkyBlue = 0xFF87CEFA;
    public static readonly StandardColors LightSlateGray = 0xFF778899;
    public static readonly StandardColors LightSteelBlue = 0xFFB0C4DE;
    public static readonly StandardColors LightYellow = 0xFFFFFFE0;
    public static readonly StandardColors Lime = 0xFF00FF00;
    public static readonly StandardColors LimeGreen = 0xFF32CD32;
    public static readonly StandardColors Linen = 0xFFFAF0E6;
    public static readonly StandardColors Magenta = 0xFFFF00FF;
    public static readonly StandardColors Maroon = 0xFF800000;
    public static readonly StandardColors MediumAquamarine = 0xFF66CDAA;
    public static readonly StandardColors MediumBlue = 0xFF0000CD;
    public static readonly StandardColors MediumOrchid = 0xFFBA55D3;
    public static readonly StandardColors MediumPurple = 0xFF9370DB;
    public static readonly StandardColors MediumSeaGreen = 0xFF3CB371;
    public static readonly StandardColors MediumSlateBlue = 0xFF7B68EE;
    public static readonly StandardColors MediumSpringGreen = 0xFF00FA9A;
    public static readonly StandardColors MediumTurquoise = 0xFF48D1CC;
    public static readonly StandardColors MediumVioletRed = 0xFFC71585;
    public static readonly StandardColors MidnightBlue = 0xFF191970;
    public static readonly StandardColors MintCream = 0xFFF5FFFA;
    public static readonly StandardColors MistyRose = 0xFFFFE4E1;
    public static readonly StandardColors Moccasin = 0xFFFFE4B5;
    public static readonly StandardColors NavajoWhite = 0xFFFFDEAD;
    public static readonly StandardColors Navy = 0xFF000080;
    public static readonly StandardColors OldLace = 0xFFFDF5E6;
    public static readonly StandardColors Olive = 0xFF808000;
    public static readonly StandardColors OliveDrab = 0xFF6B8E23;
    public static readonly StandardColors Orange = 0xFFFFA500;
    public static readonly StandardColors OrangeRed = 0xFFFF4500;
    public static readonly StandardColors Orchid = 0xFFDA70D6;
    public static readonly StandardColors PaleGoldenrod = 0xFFEEE8AA;
    public static readonly StandardColors PaleGreen = 0xFF98FB98;
    public static readonly StandardColors PaleTurquoise = 0xFFAFEEEE;
    public static readonly StandardColors PaleVioletRed = 0xFFDB7093;
    public static readonly StandardColors PapayaWhip = 0xFFFFEFD5;
    public static readonly StandardColors PeachPuff = 0xFFFFDAB9;
    public static readonly StandardColors Peru = 0xFFCD853F;
    public static readonly StandardColors Pink = 0xFFFFC0CB;
    public static readonly StandardColors Plum = 0xFFDDA0DD;
    public static readonly StandardColors PowderBlue = 0xFFB0E0E6;
    public static readonly StandardColors Purple = 0xFF800080;
    public static readonly StandardColors Red = 0xFFFF0000;
    public static readonly StandardColors RosyBrown = 0xFFBC8F8F;
    public static readonly StandardColors RoyalBlue = 0xFF4169E1;
    public static readonly StandardColors SaddleBrown = 0xFF8B4513;
    public static readonly StandardColors Salmon = 0xFFFA8072;
    public static readonly StandardColors SandyBrown = 0xFFF4A460;
    public static readonly StandardColors SeaGreen = 0xFF2E8B57;
    public static readonly StandardColors SeaShell = 0xFFFFF5EE;
    public static readonly StandardColors Sienna = 0xFFA0522D;
    public static readonly StandardColors Silver = 0xFFC0C0C0;
    public static readonly StandardColors SkyBlue = 0xFF87CEEB;
    public static readonly StandardColors SlateBlue = 0xFF6A5ACD;
    public static readonly StandardColors SlateGray = 0xFF708090;
    public static readonly StandardColors Snow = 0xFFFFFAFA;
    public static readonly StandardColors SpringGreen = 0xFF00FF7F;
    public static readonly StandardColors SteelBlue = 0xFF4682B4;
    public static readonly StandardColors Tan = 0xFFD2B48C;
    public static readonly StandardColors Teal = 0xFF008080;
    public static readonly StandardColors Thistle = 0xFFD8BFD8;
    public static readonly StandardColors Tomato = 0xFFFF6347;
    public static readonly StandardColors Transparent = 0x00FFFFFF;
    public static readonly StandardColors Turquoise = 0xFF40E0D0;
    public static readonly StandardColors Violet = 0xFFEE82EE;
    public static readonly StandardColors Wheat = 0xFFF5DEB3;
    public static readonly StandardColors White = 0xFFFFFFFF;
    public static readonly StandardColors WhiteSmoke = 0xFFF5F5F5;
    public static readonly StandardColors Yellow = 0xFFFFFF00;
    public static readonly StandardColors YellowGreen = 0xFF9ACD32;

    private static readonly FrozenSet<StandardColors> StandardColorArgbLookup =
    [
        AliceBlue,
        AntiqueWhite,
        Aqua,
        Aquamarine,
        Azure,
        Beige,
        Bisque,
        Black,
        BlanchedAlmond,
        Blue,
        BlueViolet,
        Brown,
        BurlyWood,
        CadetBlue,
        Chartreuse,
        Chocolate,
        Coral,
        CornflowerBlue,
        Cornsilk,
        Crimson,
        Cyan,
        DarkBlue,
        DarkCyan,
        DarkGoldenrod,
        DarkGray,
        DarkGreen,
        DarkKhaki,
        DarkMagenta,
        DarkOliveGreen,
        DarkOrange,
        DarkOrchid,
        DarkRed,
        DarkSalmon,
        DarkSeaGreen,
        DarkSlateBlue,
        DarkSlateGray,
        DarkTurquoise,
        DarkViolet,
        DeepPink,
        DeepSkyBlue,
        DimGray,
        DodgerBlue,
        Firebrick,
        FloralWhite,
        ForestGreen,
        Fuchsia,
        Gainsboro,
        GhostWhite,
        Gold,
        Goldenrod,
        Gray,
        Green,
        GreenYellow,
        Honeydew,
        HotPink,
        IndianRed,
        Indigo,
        Ivory,
        Khaki,
        Lavender,
        LavenderBlush,
        LawnGreen,
        LemonChiffon,
        LightBlue,
        LightCoral,
        LightCyan,
        LightGoldenrodYellow,
        LightGray,
        LightGreen,
        LightPink,
        LightSalmon,
        LightSeaGreen,
        LightSkyBlue,
        LightSlateGray,
        LightSteelBlue,
        LightYellow,
        Lime,
        LimeGreen,
        Linen,
        Magenta,
        Maroon,
        MediumAquamarine,
        MediumBlue,
        MediumOrchid,
        MediumPurple,
        MediumSeaGreen,
        MediumSlateBlue,
        MediumSpringGreen,
        MediumTurquoise,
        MediumVioletRed,
        MidnightBlue,
        MintCream,
        MistyRose,
        Moccasin,
        NavajoWhite,
        Navy,
        OldLace,
        Olive,
        OliveDrab,
        Orange,
        OrangeRed,
        Orchid,
        PaleGoldenrod,
        PaleGreen,
        PaleTurquoise,
        PaleVioletRed,
        PapayaWhip,
        PeachPuff,
        Peru,
        Pink,
        Plum,
        PowderBlue,
        Purple,
        Red,
        RosyBrown,
        RoyalBlue,
        SaddleBrown,
        Salmon,
        SandyBrown,
        SeaGreen,
        SeaShell,
        Sienna,
        Silver,
        SkyBlue,
        SlateBlue,
        SlateGray,
        Snow,
        SpringGreen,
        SteelBlue,
        Tan,
        Teal,
        Thistle,
        Tomato,
        Transparent,
        Turquoise,
        Violet,
        Wheat,
        White,
        WhiteSmoke,
        Yellow,
        YellowGreen
    ];
}