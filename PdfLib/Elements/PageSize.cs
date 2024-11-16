using PdfLib.Drawing;

namespace PdfLib.Elements;

public readonly record struct PageSize
{
    public Size Size { get; }

    private PageSize(Size size) => Size = size;

    public static readonly PageSize Letter = new(new Size(Dimension.FromInches(8.5), Dimension.FromInches(11)));
    public static readonly PageSize A4 = new(new Size(Dimension.FromMillimeters(210), Dimension.FromMillimeters(297)));
}