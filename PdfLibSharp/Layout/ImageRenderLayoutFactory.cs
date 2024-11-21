using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal class ImageRenderLayoutFactory : BaseRenderLayoutFactory
{
    protected override Content BuildContent(ContentModel contentModel, Rectangle contentBounds)
    {
        if (contentModel is not ImageContentModel imageContentModel)
            throw new ArgumentException("ContentModel must be of type ImageContentModel", nameof(contentModel));
        
        return new ImageContent(imageContentModel.Image, contentBounds, imageContentModel.BorderPen);
    }
}