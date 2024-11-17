using PdfLib.Drawing;
using PdfLib.Elements.Content;
using PdfLib.Elements;

namespace PdfLib.Layout;

internal static class ImageElementExtensions
{
    internal static ILayoutBuilder GetLayoutBuilder(this IImageElement imageElement)
    {
        Size contentSize = imageElement.GetContentSize();
        return new ImageLayoutBuilder(imageElement, contentSize);
    }

    internal static Size GetContentSize(this IImageElement imageElement)
    {
        return imageElement.GetSize(imageElement.Image.Size);
    }
}