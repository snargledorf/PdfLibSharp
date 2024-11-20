using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal static class ImageElementExtensions
{
    internal static ILayoutFactory CreateLayoutFactory(this IImageElement imageElement)
    {
        Size contentSize = imageElement.GetContentSize();
        return new ImageLayoutFactory(imageElement);
    }

    internal static Size GetContentSize(this IImageElement imageElement)
    {
        return imageElement.GetSize(imageElement.Image.Size);
    }
}