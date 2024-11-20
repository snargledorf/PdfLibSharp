using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayout(
    IStackContainer stackContainer,
    Size contentSize,
    Pen? borderPen,
    IReadOnlyList<ILayout> childLayouts)
    : ContainerLayout(stackContainer, contentSize, borderPen, childLayouts)
{
    public override PositionedLayout ToPositionedLayout(Rectangle contentBounds)
    {
        var childPositionedLayouts = new List<PositionedLayout>();

        Rectangle childElementBounds = contentBounds;

        PositionedLayout? previousLayout = null;
        foreach (ILayout childLayout in ChildLayouts)
        {
            if (previousLayout is not null)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    childElementBounds = new Rectangle
                    (
                        Point: childElementBounds.Point with
                        {
                            X = childElementBounds.Point.X + previousLayout.ContentBounds.Right + stackContainer.Gap
                        },
                        Size: childElementBounds.Size with
                        {
                            Width = childElementBounds.Size.Width -
                                    (previousLayout.ContentBounds.Size.Width + stackContainer.Gap)
                        }
                    );
                }
                else
                {
                    childElementBounds = new Rectangle
                    (
                        Point: childElementBounds.Point with
                        {
                            Y = childElementBounds.Point.Y + previousLayout.ContentBounds.Bottom + stackContainer.Gap
                        },
                        Size: childElementBounds.Size with
                        {
                            Height = childElementBounds.Size.Height -
                                     (previousLayout.ContentBounds.Size.Height + stackContainer.Gap)
                        }
                    );
                }
            }
            
            Point elementPoint = childElementBounds.Point;
            Size elementConstraint = childElementBounds.Size;
            
            if (stackContainer.Direction == Direction.Vertical)
            {
                switch (stackContainer.ElementAlignment)
                {
                    case ElementAlignment.Center:
                        elementPoint = elementPoint with
                        {
                            X = elementPoint.X + ((contentBounds.Size.Width - elementConstraint.Width) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        elementConstraint = elementConstraint with
                        {
                            Width = contentBounds.Size.Width,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        elementPoint = elementPoint with
                        {
                            X = elementPoint.X + (contentBounds.Size.Width - elementConstraint.Width)
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
                        elementPoint = elementPoint with
                        {
                            Y = elementPoint.Y + ((contentBounds.Size.Height - elementConstraint.Height) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        elementConstraint = elementConstraint with
                        {
                            Height = contentBounds.Size.Height,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        elementPoint = elementPoint with
                        {
                            Y = elementPoint.Y + (contentBounds.Size.Height - elementConstraint.Height)
                        };
                        break;
                    default:
                        throw new NotImplementedException(
                            $"ElementAlignment not implemented: {stackContainer.ElementAlignment}");
                }
            }

            var elementBounds = new Rectangle(elementPoint, elementConstraint);
            previousLayout = childLayout.ToPositionedLayout(elementBounds);
            childPositionedLayouts.Add(previousLayout);
        }

        return new ContainerPositionedLayout(childPositionedLayouts.ToArray(), contentBounds, BorderPen);
    }
}