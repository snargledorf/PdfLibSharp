using PdfLibrary.Drawing;
using PdfLibrary.Elements;

namespace PdfLibrary.Layout;

internal class ImageLayout(Image image, Point point, Size contentSize, Margins margins, Pen? borderPen)
    : ElementLayout(point, contentSize, margins, borderPen), IImageLayout
{
    public Image Image { get; } = image;
}