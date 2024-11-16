using PdfLib.Drawing;
using PdfLib.Elements;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

internal class StackLayoutBuilder(
    IStackContainer stackContainer,
    Size contentSize,
    ILayoutBuilder[] childLayoutBuilders) 
    : BorderLayoutBuilder(stackContainer, contentSize)
{
    private readonly IReadOnlyList<ILayoutBuilder> _contentSizedElements = childLayoutBuilders
        .Where(childLayoutBuilder => childLayoutBuilder.Element.Sizing == ElementSizing.Content)
        .ToArray();

    private Size SizeOfContentSizedElements
    {
        get
        {
            if (_contentSizedElements.Count == 0)
                return new Size();

            return _contentSizedElements
                .Select(childLayoutBuilder => childLayoutBuilder.OuterSize)
                .GetCombinedSize(stackContainer.Direction);
        }
    }

    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        Size fillElementSizeConstraint;
        if (FillElementsCount > 0)
            fillElementSizeConstraint = CalculateSpaceAvailableForFillElements(bounds) / FillElementsCount;
        else
            fillElementSizeConstraint = new Size();

        var contentConstrains = new Size
        (
            Width: bounds.Size.Width - Element.Margins.Left - Element.Margins.Right,
            Height: bounds.Size.Height - Element.Margins.Top - Element.Margins.Bottom
        );

        var contentBounds = new Rectangle
        (
            Point: new Point
            (
                X: bounds.Point.X + Element.Margins.Left,
                Y: bounds.Point.Y + Element.Margins.Top
            ),
            Size: contentConstrains
        );
        
        ILayout[] childLayouts = BuildChildLayouts(contentBounds, fillElementSizeConstraint).ToArray();
        
        return new ContainerLayout(bounds.Point, bounds.Size, stackContainer.Margins, borderPen, childLayouts.ToArray());
    }

    private Size CalculateSpaceAvailableForFillElements(Rectangle bounds)
    {
        return bounds.Size - SizeOfContentSizedElements;
    }

    private int FillElementsCount => childLayoutBuilders.Length - _contentSizedElements.Count;

    private IEnumerable<ILayout> BuildChildLayouts(Rectangle contentBounds, Size fillElementsSizeConstraint)
    {
        bool first = true;
        foreach (ILayoutBuilder childLayoutBuilder in childLayoutBuilders)
        {
            Point startOfNextChild = contentBounds.Point;
            
            if (!first)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                    startOfNextChild = startOfNextChild with { X = startOfNextChild.X + stackContainer.Gap };
                else
                    startOfNextChild = startOfNextChild with { Y = startOfNextChild.Y + stackContainer.Gap };
            }
            else
            {
                first = false;
            }

            Size childOuterSize = childLayoutBuilder.OuterSize;
            
            if (stackContainer.Direction == Direction.Vertical)
            {
                switch (stackContainer.ElementAlignment)
                {
                    case ElementAlignment.Center:
                        startOfNextChild = startOfNextChild with
                        {
                            X = startOfNextChild.X + ((contentBounds.Size.Width - childOuterSize.Width) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        childOuterSize = childOuterSize with
                        {
                            Width = contentBounds.Size.Width,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        startOfNextChild = startOfNextChild with
                        {
                            X = startOfNextChild.X + (contentBounds.Size.Width - childOuterSize.Width)
                        };
                        break;
                    default:
                        throw new NotImplementedException(
                            $"ElementAlignment not implemented: {stackContainer.ElementAlignment}");
                }
            }
            else
            {
                switch (stackContainer.ElementAlignment)
                {
                    case ElementAlignment.Center:
                        startOfNextChild = startOfNextChild with
                        {
                            Y = startOfNextChild.Y + ((contentBounds.Size.Height - childOuterSize.Height) / 2)
                        };
                        break;
                    case ElementAlignment.Stretch:
                        childOuterSize = childOuterSize with
                        {
                            Height = contentBounds.Size.Height,
                        };
                        break;
                    case ElementAlignment.Left:
                        break;
                    case ElementAlignment.Right:
                        startOfNextChild = startOfNextChild with
                        {
                            Y = startOfNextChild.Y + (contentBounds.Size.Height - childOuterSize.Height)
                        };
                        break;
                    default:
                        throw new NotImplementedException(
                            $"ElementAlignment not implemented: {stackContainer.ElementAlignment}");
                }
            }
            
            if (childLayoutBuilder.Element.Sizing == ElementSizing.ExpandToFillBounds)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    childOuterSize = childOuterSize with
                    {
                        Width = fillElementsSizeConstraint.Width,
                    };
                }
                else
                {
                    childOuterSize = childOuterSize with
                    {
                        Height = fillElementsSizeConstraint.Height,
                    };
                }
            }

            var childBounds = new Rectangle(startOfNextChild, childOuterSize);
            ILayout childLayout = childLayoutBuilder.BuildLayout(childBounds);
            yield return childLayout;

            if (stackContainer.Direction == Direction.Horizontal)
                contentBounds = contentBounds with
                {
                    Point = contentBounds.Point with
                    {
                        X = childLayout.OuterBounds.Right
                    },
                };
            else
                contentBounds = contentBounds with
                {
                    Point = contentBounds.Point with
                    {
                        Y = childLayout.OuterBounds.Bottom
                    }
                };
        }
    }
}