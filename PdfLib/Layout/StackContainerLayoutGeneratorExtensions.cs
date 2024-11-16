using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Content;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal static class StackContainerLayoutGeneratorExtensions
{
    public static ILayoutBuilder GetLayoutBuilder(this IStackContainer stackContainer, Font font,
        StringFormat stringFormat, IMeasureGraphics measureGraphics)
    {
        font = stackContainer.GetFont(font);
        stringFormat = stackContainer.StringFormat ?? stringFormat;

        ILayoutBuilder[] childLayoutBuilders = stackContainer
            .GetChildLayoutBuilders(font, stringFormat, measureGraphics)
            .ToArray();

        Size contentSize = childLayoutBuilders
            .Select(childLayoutBuilder => childLayoutBuilder.OuterSize)
            .GetCombinedSize(stackContainer.Direction);

        return new StackLayoutBuilder(stackContainer, contentSize, childLayoutBuilders);
    }

    public static IEnumerable<ILayoutBuilder> GetChildLayoutBuilders(
        this IStackContainer stackContainer, 
        Font font,
        StringFormat stringFormat,
        IMeasureGraphics measureGraphics)
    {
        foreach (IElement element in stackContainer.Elements)
        {
            yield return element switch
            {
                ITextElement textElement => textElement.GetLayoutBuilder(font, stringFormat, measureGraphics),
                IImageElement imageElement => imageElement.GetLayoutBuilder(),
                IStackContainer childStack => childStack.GetLayoutBuilder(font, stringFormat, measureGraphics),
                _ => throw new InvalidOperationException("Unexpected element type during content size calculation")
            };
        }
    }

    public static Size GetCombinedSize(this IEnumerable<Size> elementSizes, Direction direction)
    {
        return elementSizes.Aggregate((current, next) =>
        {
            return direction switch
            {
                Direction.Horizontal => new Size
                (
                    Width: current.Width + next.Width,
                    Height: Math.Max(current.Height, next.Height)
                ),
                Direction.Vertical => new Size
                (
                    Height: current.Height + next.Height,
                    Width: Math.Max(current.Width, next.Width)
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };
        });
    }
}