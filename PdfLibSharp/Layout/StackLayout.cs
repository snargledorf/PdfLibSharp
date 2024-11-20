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
            
            Point childPoint = currentContentBounds.Point;
            Size childOuterConstraint = childLayout.ContentSize + childLayout.Margins.ToSize();
            
            if (stackContainer.Direction == Direction.Vertical)
            {
                switch (stackContainer.ElementAlignment)
                {
                    case ElementAlignment.Center:
                        childPoint = childPoint with
                        {
                            X = childPoint.X + ((currentContentBounds.Size.Width - childOuterConstraint.Width) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        childOuterConstraint = childOuterConstraint with
                        {
                            Width = currentContentBounds.Size.Width,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        childPoint = childPoint with
                        {
                            X = childPoint.X + (currentContentBounds.Size.Width - childOuterConstraint.Width)
                        };
                        break;
                    default:
                        throw new NotImplementedException(
                            $"ElementAlignment not implemented: {stackContainer.ElementAlignment}");
                }
            }
            else // Horizontal
            {
                switch (stackContainer.ElementAlignment)
                {
                    case ElementAlignment.Center:
                        childPoint = childPoint with
                        {
                            Y = childPoint.Y + ((currentContentBounds.Size.Height - childOuterConstraint.Height) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        childOuterConstraint = childOuterConstraint with
                        {
                            Height = currentContentBounds.Size.Height,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        childPoint = childPoint with
                        {
                            Y = childPoint.Y + (currentContentBounds.Size.Height - childOuterConstraint.Height)
                        };
                        break;
                    default:
                        throw new NotImplementedException(
                            $"ElementAlignment not implemented: {stackContainer.ElementAlignment}");
                }
            }

            childOuterConstraint = StackLayoutUtilities.UpdateConstraintsForElementSizing(
                childOuterConstraint,
                childLayout.Sizing,
                fillElementsSizeConstraint,
                currentContentBounds.Size,
                stackContainer.Direction
            );

            var childBounds = new Rectangle(childPoint, childOuterConstraint);
            previousLayout = childLayout.ToPositionedLayout(childBounds);
            childPositionedLayouts.Add(previousLayout);
        }

        return new ContainerContent(childPositionedLayouts.ToArray(), BorderPen);
    }
}