using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayout(
    IStackContainer stackContainer,
    Size contentSize,
    Pen? borderPen,
    IReadOnlyList<ILayout> childLayouts)
    : ContainerLayout(stackContainer, contentSize, borderPen, childLayouts)
{
    protected override object BuildContent(Rectangle contentBounds)
    {
        Size fillElementsSizeConstraint = StackLayoutUtilities.CalculateFillElementsSizeConstraint(
            ChildLayouts,
            contentBounds.Size,
            stackContainer.Direction,
            stackContainer.Gap
        );
        
        var childPositionedLayouts = new List<PositionedLayout>();
        
        Rectangle currentContentBounds = contentBounds;

        PositionedLayout? previousLayout = null;
        foreach (ILayout childLayout in ChildLayouts)
        {
            if (previousLayout is not null)
            {
                currentContentBounds = currentContentBounds with
                {
                    Size = StackLayoutUtilities.UpdateConstraints(
                        currentContentBounds.Size,
                        previousLayout.OuterBounds.Size,
                        stackContainer.Direction,
                        stackContainer.Gap
                    )
                };
                
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    currentContentBounds = currentContentBounds with
                    {
                        Point = currentContentBounds.Point with
                        {
                            X = previousLayout.OuterBounds.Right + stackContainer.Gap
                        }
                    };
                }
                else
                {
                    currentContentBounds = currentContentBounds with
                    {
                        Point = currentContentBounds.Point with
                        {
                            Y = previousLayout.OuterBounds.Bottom + stackContainer.Gap
                        }
                    };
                }
            }

            Size childOuterSize = DetermineChildSizeOuterSize(
                childLayout.ContentSize + childLayout.Margins.ToSize(),
                fillElementsSizeConstraint,
                currentContentBounds.Size,
                childLayout.Sizing
            );
            
            Point childPoint = DetermineChildPoint(currentContentBounds, childOuterSize);

            var childBounds = new Rectangle(childPoint, childOuterSize);
            
            previousLayout = childLayout.ToPositionedLayout(childBounds);
            childPositionedLayouts.Add(previousLayout);
        }

        return new ContainerContent(childPositionedLayouts.ToArray(), BorderPen);
    }

    private Size DetermineChildSizeOuterSize(Size childCurrentOuterSize, Size fillElementsSizeConstraint, Size constraints, ElementSizing elementSizing)
    {
        if (stackContainer.ElementAlignment == ElementAlignment.Stretch)
        {
            if (stackContainer.Direction == Direction.Vertical)
            {
                childCurrentOuterSize = childCurrentOuterSize with
                {
                    Width = constraints.Width,
                };
            }
            else // Horizontal
            {
                childCurrentOuterSize = childCurrentOuterSize with
                {
                    Height = constraints.Height,
                };
            }
        }

        return StackLayoutUtilities.UpdateConstraintsForElementSizing(
            childCurrentOuterSize,
            elementSizing,
            fillElementsSizeConstraint,
            constraints,
            stackContainer.Direction
        );
    }

    private Point DetermineChildPoint(Rectangle bounds, Size childOuterSize)
    {
        if (stackContainer.Direction == Direction.Vertical)
        {
            switch (stackContainer.ElementAlignment)
            {
                case ElementAlignment.Center:
                    return bounds.Point with
                    {
                        X = bounds.Point.X + ((bounds.Size.Width - childOuterSize.Width) / 2)
                    };
                case ElementAlignment.Right:
                    return bounds.Point with
                    {
                        X = bounds.Point.X + (bounds.Size.Width - childOuterSize.Width)
                    };
            }
        }
        else // Horizontal
        {
            switch (stackContainer.ElementAlignment)
            {
                case ElementAlignment.Center:
                    return bounds.Point with
                    {
                        Y = bounds.Point.Y + ((bounds.Size.Height - childOuterSize.Height) / 2)
                    };
                case ElementAlignment.Right:
                    return bounds.Point with
                    {
                        Y = bounds.Point.Y + (bounds.Size.Height - childOuterSize.Height)
                    };
            }
        }

        return bounds.Point;
    }
}