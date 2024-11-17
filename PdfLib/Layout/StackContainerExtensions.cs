using PdfLib.Drawing;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal static class StackContainerExtensions
{
    public static ILayoutBuilder GetLayoutBuilder(this IStackContainer stackContainer, Font font,
        StringFormat stringFormat, ILayoutBuilderFactory iLayoutBuilderFactory)
    {
        font = stackContainer.GetFont(font);
        stringFormat = stackContainer.StringFormat ?? stringFormat;

        ILayoutBuilder[] childLayoutBuilders = stackContainer.Elements
            .Select(element => iLayoutBuilderFactory.GetLayoutBuilder(element, font, stringFormat))
            .ToArray();

        Size contentSize = childLayoutBuilders
            .Select(childLayoutBuilder => childLayoutBuilder.OuterSize)
            .GetCombinedSize(stackContainer.Direction);

        return new StackLayoutBuilder(stackContainer, contentSize, childLayoutBuilders);
    }
}