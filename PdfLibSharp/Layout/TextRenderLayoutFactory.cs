using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal sealed class TextRenderLayoutFactory : BaseRenderLayoutFactory
{
    protected override Content BuildContent(ContentModel contentModel, Rectangle contentBounds)
    {
        if (contentModel is not TextContentModel textContentModel)
            throw new ArgumentException("Model is not a TextContentModel", nameof(contentModel));

        var positionedLines = new List<PositionedTextLine>();

        Point linePoint = contentBounds.Point;

        PositionedTextLine? previousLineLayout = null;
        foreach (TextLine line in textContentModel.Lines)
        {
            if (previousLineLayout is not null)
            {
                linePoint = linePoint with
                {
                    Y = previousLineLayout.ContentBounds.Bottom
                };
            }

            Size lineContentSize = line.Size with
            {
                Width = contentBounds.Size.Width
            };

            var lineBounds = new Rectangle(linePoint, lineContentSize);

            previousLineLayout = new PositionedTextLine(line.Text, lineBounds);
            positionedLines.Add(previousLineLayout);
        }

        return new TextContent(
            contentBounds,
            positionedLines.ToArray(),
            textContentModel.Font,
            textContentModel.Format,
            textContentModel.Brush,
            textContentModel.BorderPen
        );
    }
}