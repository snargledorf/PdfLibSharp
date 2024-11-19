using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

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