using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class LineRenderLayoutFactory : BaseRenderLayoutFactory
{
    protected override Content BuildContent(ContentModel contentModel, Rectangle contentBounds)
    {
        if (contentModel is not LineContentModel lineContentModel)
            throw new ArgumentException("ContentModel must be of type LineContentModel", nameof(contentModel));

        Point start = contentBounds.Point;

        Point end = lineContentModel.Direction == Direction.Horizontal
            ? contentBounds.TopRight
            : contentBounds.BottomLeft;

        return new LineContent(contentBounds, start, end, lineContentModel.Pen);
    }
}