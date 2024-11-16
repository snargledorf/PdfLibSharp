using PdfLibrary.Drawing;
using PdfLibrary.Elements;
using PdfLibrary.Elements.Content;

namespace PdfLibrary.Layout;

internal static class ImageLayoutGeneratorExtensions
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