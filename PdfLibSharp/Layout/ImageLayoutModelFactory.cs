using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal class ImageLayoutModelFactory : BorderLayoutModelFactory
{
    protected override ContentModel CreateBorderedContentModel(IBorderElement borderElement, Pen? borderPen, Size constraints,
        LayoutScope scope)
    {
        if (borderElement is not IImageElement imageElement)
            throw new ArgumentException("Element must implement IImageElement", nameof(borderElement));
        
        // TODO: Resize image to fit constraints
        return new ImageContentModel(imageElement.Image, imageElement.GetSize(imageElement.Image.Size), borderPen);
    }
}