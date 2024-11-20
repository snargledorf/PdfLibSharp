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
        Size fillElementsSizeConstraint = CalculateFillElementsSizeConstraint(contentBounds.Size);
        
        var childPositionedLayouts = new List<PositionedLayout>();
        
        Rectangle currentContentBounds = contentBounds;

        PositionedLayout? previousLayout = null;
        foreach (ILayout childLayout in ChildLayouts)
        {
            if (previousLayout is not null)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    currentContentBounds = new Rectangle
                    (
                        Point: currentContentBounds.Point with
                        {
                            X = previousLayout.OuterBounds.Right + stackContainer.Gap
                        },
                        Size: currentContentBounds.Size with
                        {
                            Width = currentContentBounds.Size.Width -
                                    (previousLayout.OuterBounds.Size.Width + stackContainer.Gap)
                        }
                    );
                }
                else
                {
                    currentContentBounds = new Rectangle
                    (
                        Point: currentContentBounds.Point with
                        {
                            Y = previousLayout.OuterBounds.Bottom + stackContainer.Gap
                        },
                        Size: currentContentBounds.Size with
                        {
                            Height = currentContentBounds.Size.Height -
                                     (previousLayout.OuterBounds.Size.Height + stackContainer.Gap)
                        }
                    );
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

            if (childLayout.Sizing == ElementSizing.ExpandToFillBounds)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    childOuterConstraint = childOuterConstraint with
                    {
                        Width = Math.Min(fillElementsSizeConstraint.Width, currentContentBounds.Size.Width)
                    };
                }
                else
                {
                    childOuterConstraint = childOuterConstraint with
                    {
                        Height = Math.Min(fillElementsSizeConstraint.Height, currentContentBounds.Size.Height)
                    };
                }
            }

            var childBounds = new Rectangle(childPoint, childOuterConstraint);
            previousLayout = childLayout.ToPositionedLayout(childBounds);
            childPositionedLayouts.Add(previousLayout);
        }

        return new ContainerContent(childPositionedLayouts.ToArray(), BorderPen);
    }

    private Size CalculateFillElementsSizeConstraint(Size constraints)
    {
        int fillElementsCount =
            ChildLayouts.Count(layout => layout.Sizing == ElementSizing.ExpandToFillBounds);
        
        if (fillElementsCount == 0)
            return new Size();

        IReadOnlyList<ILayout> contentSizedLayouts =
            ChildLayouts.Where(layout => layout.Sizing == ElementSizing.Content).ToArray();

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
}