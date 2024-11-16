using PdfLib.Drawing;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal class ImageLayout(Image image, Point point, Size contentSize, Margins margins, Pen? borderPen)
    : ElementLayout(point, contentSize, margins, borderPen), IImageLayout
{
    public Image Image { get; } = image;
}