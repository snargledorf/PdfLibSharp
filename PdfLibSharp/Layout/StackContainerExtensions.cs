using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal static class StackContainerExtensions
{
    public static ILayoutBuilder GetLayoutBuilder(this IStackContainer stackContainer, LayoutScope scope, ILayoutBuilderFactory layoutBuilderFactory)
    {
        scope = new LayoutScope
        (
            stackContainer.GetFont(scope.Font),
            stackContainer.LineHeight ?? scope.LineHeight,
            stackContainer.StringFormat ?? scope.StringFormat,
            stackContainer.FontColor ?? scope.FontColor
        );

        ILayoutBuilder[] childLayoutBuilders = stackContainer.Elements
            .Select(element => layoutBuilderFactory.GetLayoutBuilder(element, scope))
            .ToArray();

        Size contentSize = childLayoutBuilders
            .Select(childLayoutBuilder => childLayoutBuilder.OuterSize)
            .GetCombinedSize(stackContainer.Direction);

        contentSize = stackContainer.GetSize(contentSize);

        return new StackLayoutBuilder(stackContainer, contentSize, childLayoutBuilders);
    }
}