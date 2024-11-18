using PdfLib.Elements.Content;
using PdfSharp.Drawing;

namespace PdfLib.Drawing;

public sealed class StringFormat
{
    private readonly XStringFormat _stringFormat;

    public static readonly StringFormat BaseLineLeft = XStringFormats.BaseLineLeft;

    public static readonly StringFormat TopLeft = XStringFormats.TopLeft;
    
    public static readonly StringFormat TopCenter = XStringFormats.TopCenter;
    
    public static readonly StringFormat TopRight = XStringFormats.TopRight;

    public static readonly StringFormat CenterRight = XStringFormats.CenterRight;

    public static readonly StringFormat Center = XStringFormats.Center;


    private StringFormat(XStringFormat stringFormat)
    {
        _stringFormat = stringFormat;
    }

    public LineAlignment LineAlignment => (LineAlignment)_stringFormat.LineAlignment;

    public StringAlignment Alignment => (StringAlignment)_stringFormat.Alignment;

    public static implicit operator XStringFormat(StringFormat stringFormat) => stringFormat._stringFormat;
    public static implicit operator StringFormat(XStringFormat stringFormat) => new(stringFormat);
}