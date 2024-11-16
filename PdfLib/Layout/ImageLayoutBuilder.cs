using PdfLib.Drawing;
using PdfLib.Elements.Content;

namespace PdfLib.Layout;

internal class ImageLayoutBuilder(IImageElement imageElement, Size contentSize) : BorderLayoutBuilder(imageElement, contentSize)
{
    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        return new ImageLayout(imageElement.Image, bounds.Point, ContentSize, imageElement.Margins, borderPen);
    }
}