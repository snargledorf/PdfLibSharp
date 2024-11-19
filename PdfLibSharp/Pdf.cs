using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp;

public sealed class Pdf
{
    private readonly List<IPage> _pages = [];
    private Font _defaultFont = new("Times New Roman", 12f);
    private StringFormat _defaultStringFormat = StringFormat.BaseLineLeft;

    public Font DefaultFont
    {
        get => _defaultFont;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _defaultFont = value;
        }
    }

    public PageSize DefaultPageSize { get; set; } = PageSize.A4;
    
    public IReadOnlyList<IPage> Pages => _pages.AsReadOnly();

    public StringFormat DefaultStringFormat
    {
        get => _defaultStringFormat;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _defaultStringFormat = value;
        }
    }

    public Color DefaultFontColor { get; set; } = Color.Black;
    public double DefaultLineHeight { get; set; } = 1;

    public IPage AddPage()
    {
        var page = new Page(this);
        _pages.Add(page);
        return page;
    }
}