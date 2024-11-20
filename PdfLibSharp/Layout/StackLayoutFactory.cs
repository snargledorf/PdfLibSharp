using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayoutFactory(IStackContainer stackContainer, ILayoutFactory[] childLayoutFactories)
    : BorderLayoutFactory(stackContainer)
{
    protected override ILayout CreateInnerLayout(Size constraints)
    {
        Size fillElementsSizeConstraint = CalculateFillElementsSizeConstraint(constraints);
        
        ILayout[] childLayouts = BuildChildLayouts(childLayoutFactories, constraints, fillElementsSizeConstraint).ToArray();

        Size contentSize = childLayouts
            .Select(layout => layout.ContentSize + layout.Margins.ToSize())
            .GetCombinedSize(stackContainer.Direction, stackContainer.Gap);

       return new StackLayout(stackContainer, stackContainer.GetSize(contentSize), BorderPen, childLayouts);
    }

    private Size CalculateFillElementsSizeConstraint(Size constraints)
    {
        int fillElementsCount =
            childLayoutFactories.Count(factory => factory.Element.Sizing == ElementSizing.ExpandToFillBounds);
        
        if (fillElementsCount == 0)
            return new Size();

        IEnumerable<ILayoutFactory> contentSizedLayoutFactories = childLayoutFactories.Where(factory => factory.Element.Sizing == ElementSizing.Content);
        IReadOnlyList<ILayout> contentSizedLayouts = BuildChildLayouts(contentSizedLayoutFactories, constraints, new Size()).ToArray();

        Size outerSizeOfContentLayoutsWithGapSum = contentSizedLayouts
            .Select(layout => layout.ContentSize + layout.Margins.ToSize())
            .GetCombinedSize(stackContainer.Direction, stackContainer.Gap);

        if (stackContainer.Direction == Direction.Horizontal)
        {
            return constraints with
            {
                Width = (constraints.Width - outerSizeOfContentLayoutsWithGapSum.Width) / fillElementsCount
            };
        }

        return constraints with
        {
            Height = (constraints.Height - outerSizeOfContentLayoutsWithGapSum.Height) / fillElementsCount
        };
    }

    private IEnumerable<ILayout> BuildChildLayouts(IEnumerable<ILayoutFactory> layoutFactories, Size constraints, Size fillElementsSizeConstraint)
    {
        ILayout? previousLayout = null;
        foreach (ILayoutFactory layoutFactory in layoutFactories)
        {
            if (previousLayout is not null)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    Dimension widthUsed = (previousLayout.ContentSize + previousLayout.Margins.ToSize()).Width +
                                          stackContainer.Gap;
                    constraints = constraints with
                    {
                        Width = constraints.Width - widthUsed
                    };
                }
                else
                {
                    Dimension heightUsed = (previousLayout.ContentSize + previousLayout.Margins.ToSize()).Height +
                                           stackContainer.Gap;
                    constraints = constraints with
                    {
                        Height = constraints.Height - heightUsed
                    };
                }
            }

            Size elementConstraints = constraints;

            if (layoutFactory.Element.Sizing == ElementSizing.ExpandToFillBounds)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    elementConstraints = elementConstraints with
                    {
                        Width = Math.Min(fillElementsSizeConstraint.Width, elementConstraints.Width)
                    };
                }
                else
                {
                    elementConstraints = elementConstraints with
                    {
                        Height = Math.Min(fillElementsSizeConstraint.Height, elementConstraints.Height)
                    };
                }
            }
            
            previousLayout = layoutFactory.CreateLayout(elementConstraints);
            yield return previousLayout;
        }
    }
}