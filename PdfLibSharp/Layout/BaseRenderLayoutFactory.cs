using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal abstract class BaseRenderLayoutFactory : IRenderLayoutFactory
{
    public RenderLayout CreateRenderLayout(LayoutModel model, Rectangle bounds)
    {
        Size innerBoundsSize = bounds.Size - model.Margins.ToSize();
        
        var contentBounds = new Rectangle
        (
            Point: bounds.Point + new Point(model.Margins.Left, model.Margins.Top),
            Size: model.GetSize(innerBoundsSize)
        );
        
        Content content = BuildContent(model.ContentModel, contentBounds);
        
        return new RenderLayout(content, bounds);
    }

    protected abstract Content BuildContent(ContentModel contentModel, Rectangle contentBounds);
}