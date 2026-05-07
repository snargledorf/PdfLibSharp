using PdfLibSharp.Elements;

namespace PdfLibSharp.Drawing;

public sealed class StringFormat
{
    public static StringFormat BaseLineLeft => new()
    {
        LineAlignment = LineAlignment.Baseline,
        Alignment = StringAlignment.Near
    };

    public static StringFormat TopLeft => new()
    {
        LineAlignment = LineAlignment.Near,
        Alignment = StringAlignment.Near
    };
    
    public static StringFormat TopCenter => new()
    {
        LineAlignment = LineAlignment.Near,
        Alignment = StringAlignment.Center
    };

    public static StringFormat TopRight => new()
    {
        LineAlignment = LineAlignment.Near,
        Alignment = StringAlignment.Far
    };

    public static StringFormat CenterLeft => new()
    {
        LineAlignment = LineAlignment.Center,
        Alignment = StringAlignment.Near
    };

    public static StringFormat Center => new()
    {
        LineAlignment = LineAlignment.Center,
        Alignment = StringAlignment.Center
    };

    public static StringFormat CenterRight => new()
    {
        LineAlignment = LineAlignment.Center,
        Alignment = StringAlignment.Far
    };

    public static StringFormat BottomLeft => new()
    {
        LineAlignment = LineAlignment.Far,
        Alignment = StringAlignment.Near
    };

    public static StringFormat BottomCenter => new()
    {
        LineAlignment = LineAlignment.Far,
        Alignment = StringAlignment.Center
    };

    public static StringFormat BottomRight => new()
    {
        LineAlignment = LineAlignment.Far,
        Alignment = StringAlignment.Far
    };

    public LineAlignment LineAlignment
    {
        get;
        set;
    }

    public StringAlignment Alignment
    {
        get;
        set;
    }
}