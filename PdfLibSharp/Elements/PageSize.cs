using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

public readonly record struct PageSize
{
    private readonly PdfSharp.PageSize _pageSize;
    
    public Size Size { get; }

    private PageSize(Size size, PdfSharp.PageSize pageSize)
    {
        Size = size;
        _pageSize = pageSize;
    }

    public static readonly PageSize Letter = new(new Size(Dimension.FromInches(8.5), Dimension.FromInches(11)),
        PdfSharp.PageSize.Letter);

    public static readonly PageSize A4 = new(new Size(Dimension.FromMillimeters(210), Dimension.FromMillimeters(297)),
        PdfSharp.PageSize.A4);

    public static implicit operator PdfSharp.PageSize(PageSize pageSize) => pageSize._pageSize;
}