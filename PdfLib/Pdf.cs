using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary;

public sealed class Pdf
{
    private readonly List<IPage> _pages = [];

    public Font DefaultFont { get; set; } = new("Times New Roman", 12f);
    
    public PageSize DefaultPageSize { get; set; } = PageSize.A4;
    
    public IReadOnlyList<IPage> Pages => _pages.AsReadOnly();
    public StringFormat DefaultStringFormat { get; set; } = StringFormat.BaseLineLeft;

    public IPage AddPage()
    {
        var page = new Page(this);
        _pages.Add(page);
        return page;
    }
}