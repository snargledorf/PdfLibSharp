using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayoutFactory(IStackContainer stackContainer, ILayoutFactory[] childLayoutFactories)
    : BorderLayoutFactory(stackContainer)
{
    private Dimension GapsSum
    {
        get
        {
            int gapsCount = stackContainer.Elements.Count - 1;
            return gapsCount >= 0 ? stackContainer.Gap * gapsCount : 0;
        }
    }

    protected override ILayout CreateInnerLayout(Size constraints)
    {
        Size fillElementSizeConstraint = CalculateFillElementsSizeConstraint(constraints);
        
        ILayout[] childLayouts = BuildChildLayouts(constraints, fillElementSizeConstraint).ToArray();

        Size childrenSize = GetOuterSizeOfLayouts(childLayouts);
        
        return new StackLayout(stackContainer, childrenSize, BorderPen, childLayouts);
    }

    private Size CalculateFillElementsSizeConstraint(Size constraints)
    {
        int fillElementsCount =
            childLayoutFactories.Count(lf => lf.Element.Sizing == ElementSizing.ExpandToFillBounds);
        
        if (fillElementsCount == 0)
            return new Size();

        IReadOnlyList<ILayout> contentSizedLayouts = BuildContentSizedLayouts(constraints).ToArray();

        Size outerSizeOfContentLayoutsWithGapSum = GetOuterSizeOfLayouts(contentSizedLayouts);

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

    private IEnumerable<ILayout> BuildContentSizedLayouts(Size constraints)
    {
        IEnumerable<ILayoutFactory> contentSizedElementFactories = childLayoutFactories
            .Where(lf => lf.Element.Sizing == ElementSizing.Content);

        ILayout? previousLayout = null;
        foreach (ILayoutFactory layoutFactory in contentSizedElementFactories)
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
            
            previousLayout = layoutFactory.CreateLayout(constraints);
            yield return previousLayout;
        }
    }

    private Size GetOuterSizeOfLayouts(IReadOnlyCollection<ILayout> layouts, bool includeGap = true)
    {
        if (layouts.Count == 0)
            return new Size();

        Size size = layouts
            .Select(layout => layout.ContentSize + layout.Margins.ToSize())
            .GetCombinedSize(stackContainer.Direction);

        if (!includeGap)
            return size;
        
        if (stackContainer.Direction == Direction.Horizontal)
        {
            return size with
            {
                Width = size.Width + GapsSum
            };
        }

        return size with
        {
            Height = size.Height + GapsSum
        };
    }

    private IEnumerable<ILayout> BuildChildLayouts(Size constraints, Size fillElementsSizeConstraint)
    {
        ILayout? previousLayout = null;
        foreach (ILayoutFactory layoutFactory in childLayoutFactories)
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

            Size elementSizeConstraints = constraints;

            if (layoutFactory.Element.Sizing == ElementSizing.ExpandToFillBounds)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    elementSizeConstraints = elementSizeConstraints with
                    {
                        Width = Math.Min(fillElementsSizeConstraint.Width, elementSizeConstraints.Width)
                    };
                }
                else
                {
                    elementSizeConstraints = elementSizeConstraints with
                    {
                        Height = Math.Min(fillElementsSizeConstraint.Height, elementSizeConstraints.Height)
                    };
                }
            }
            
            previousLayout = layoutFactory.CreateLayout(elementSizeConstraints);
            yield return previousLayout;
        }
    }
}