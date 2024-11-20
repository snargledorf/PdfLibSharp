using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal static class ImageElementExtensions
{
    internal static ILayoutFactory CreateLayoutFactory(this IImageElement imageElement)
    {
        return new ImageLayoutFactory(imageElement);
    }
}