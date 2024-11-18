using PdfLib.Drawing;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal static class StackContainerExtensions
{
    public static ILayoutBuilder GetLayoutBuilder(this IStackContainer stackContainer, Font font,
        Color fontColor,
        StringFormat stringFormat, ILayoutBuilderFactory layoutBuilderFactory)
    {
        font = stackContainer.GetFont(font);
        fontColor = stackContainer.FontColor ?? fontColor;
        stringFormat = stackContainer.StringFormat ?? stringFormat;

        ILayoutBuilder[] childLayoutBuilders = stackContainer.Elements
            .Select(element => layoutBuilderFactory.GetLayoutBuilder(element, font, fontColor, stringFormat))
            .ToArray();

        Size contentSize = childLayoutBuilders
            .Select(childLayoutBuilder => childLayoutBuilder.OuterSize)
            .GetCombinedSize(stackContainer.Direction);

        return new StackLayoutBuilder(stackContainer, contentSize, childLayoutBuilders);
    }
}