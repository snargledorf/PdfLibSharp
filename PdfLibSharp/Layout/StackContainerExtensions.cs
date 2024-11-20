using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal static class StackContainerExtensions
{
    public static ILayoutFactory CreateLayoutFactory(this IStackContainer stackContainer, LayoutScope scope, ILayoutBuilderFactory layoutBuilderFactory)
    {
        scope = new LayoutScope
        (
            stackContainer.GetFont(scope.Font),
            stackContainer.LineHeight ?? scope.LineHeight,
            stackContainer.StringFormat ?? scope.StringFormat,
            stackContainer.FontColor ?? scope.FontColor
        );

        ILayoutFactory[] childLayoutBuilders = stackContainer.Elements
            .Select(element => layoutBuilderFactory.CreateLayoutFactory(element, scope))
            .ToArray();

        return new StackLayoutFactory(stackContainer, childLayoutBuilders);
    }
}