using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal class ImageLayout(Image image, Point point, Size contentSize, Margins margins, Pen? borderPen)
    : BorderElementLayout(point, contentSize, margins, borderPen), IImageLayout
{
    public Image Image { get; } = image;
}