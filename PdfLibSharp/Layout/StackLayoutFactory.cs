using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayoutFactory(IStackContainer stackContainer, ILayoutFactory[] childLayoutFactories)
    : BorderLayoutFactory(stackContainer)
{
    protected override ILayout CreateInnerLayout(Size constraints)
    {
        Size fillElementsSizeConstraint = GetFillElementsSizeConstraint(constraints);

        ILayout[] childLayouts = BuildChildLayouts(childLayoutFactories, constraints, fillElementsSizeConstraint).ToArray();

        Size contentSize = childLayouts
            .Select(layout => layout.ContentSize + layout.Margins.ToSize())
            .GetCombinedSize(stackContainer.Direction, stackContainer.Gap);

       return new StackLayout(stackContainer, stackContainer.GetSize(contentSize), BorderPen, childLayouts);
    }

    private Size GetFillElementsSizeConstraint(Size constraints)
    {
        IEnumerable<ILayoutFactory> contentSizedLayoutFactories =
            childLayoutFactories.Where(factory => factory.Element.Sizing == ElementSizing.Content);
        
        IReadOnlyList<ILayout> contentSizedLayouts =
            BuildChildLayouts(contentSizedLayoutFactories, constraints, new Size()).ToArray();

        return StackLayoutUtilities.CalculateFillElementsSizeConstraint(
            contentSizedLayouts,
            constraints,
            stackContainer.Direction,
            stackContainer.Gap
        );
    }

    private IEnumerable<ILayout> BuildChildLayouts(
        IEnumerable<ILayoutFactory> layoutFactories, 
        Size constraints,
        Size fillElementsSizeConstraint)
    {
        ILayout? previousLayout = null;
        foreach (ILayoutFactory layoutFactory in layoutFactories)
        {
            if (previousLayout is not null)
            {
                Size previousLayoutOuterSize = previousLayout.ContentSize + previousLayout.Margins.ToSize();

                constraints = StackLayoutUtilities.UpdateConstraints(
                    constraints,
                    previousLayoutOuterSize,
                    stackContainer.Direction,
                    stackContainer.Gap
                );
            }

            Size elementConstraints = StackLayoutUtilities.UpdateConstraintsForElementSizing(
                constraints,
                layoutFactory.Element.Sizing,
                fillElementsSizeConstraint,
                constraints,
                stackContainer.Direction
            );

            previousLayout = layoutFactory.CreateLayout(elementConstraints);
            yield return previousLayout;
        }
    }
}