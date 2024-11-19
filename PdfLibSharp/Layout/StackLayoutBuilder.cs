using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayoutBuilder(
    IStackContainer stackContainer,
    Size contentSize,
    ILayoutBuilder[] childLayoutBuilders) 
    : BorderLayoutBuilder(stackContainer, contentSize)
{
    private readonly IReadOnlyList<ILayoutBuilder> _contentSizedElements = childLayoutBuilders
        .Where(childLayoutBuilder => childLayoutBuilder.Element.Sizing == ElementSizing.Content)
        .ToArray();

    private Dimension GapsSum
    {
        get
        {
            int gapsCount = stackContainer.Elements.Count - 1;
            return gapsCount >= 0 ? stackContainer.Gap * gapsCount : 0;
        }
    }

    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        var contentBounds = new Rectangle
        (
            Point: stackContainer.GetInnerPoint(bounds.Point),
            Size: stackContainer.GetInnerSize(bounds.Size)
        );

        Size fillElementSizeConstraint = CalculateFillElementsSizeConstraint(contentBounds);
        
        ILayout[] childLayouts = BuildChildLayouts(childLayoutBuilders, contentBounds, fillElementSizeConstraint).ToArray();

        Size childrenSize = childLayouts.Select(cl => cl.OuterBounds.Size)
            .GetCombinedSize(stackContainer.Direction);

        if (stackContainer.Direction == Direction.Horizontal)
        {
            childrenSize = childrenSize with
            {
                Width = childrenSize.Width + GapsSum
            };
        }
        else
        {
            childrenSize = childrenSize with
            {
                Height = childrenSize.Height + GapsSum
            };
        }

        bounds = bounds with
        {
            Size = new Size
            (
                Width: Math.Max(bounds.Size.Width, childrenSize.Width),
                Height: Math.Max(bounds.Size.Height, childrenSize.Height)
            )
        };
        
        return new ContainerLayout(bounds.Point, bounds.Size, stackContainer.Margins, borderPen, childLayouts.ToArray());
    }

    private Size GetSizeOfLayouts(IReadOnlyList<ILayout> layouts)
    {
        if (layouts.Count == 0)
            return new Size();

        return layouts
            .Select(layout => layout.OuterBounds.Size)
            .GetCombinedSize(stackContainer.Direction);
    }

    private Size CalculateFillElementsSizeConstraint(Rectangle contentBounds)
    {
        if (FillElementsCount <= 0)
            return new Size();

        // TODO: Refactor to not build layouts twice. Maybe add a way to update bounds of layout after being built?
        IReadOnlyList<ILayout> contentSizedElementLayouts = BuildChildLayouts(_contentSizedElements, contentBounds, new Size()).ToArray();

        Size sizeOfContentSizedElements = GetSizeOfLayouts(contentSizedElementLayouts);

        Size contentSizeConstraint = contentBounds.Size;

        if (stackContainer.Direction == Direction.Horizontal)
        {
            return contentSizeConstraint with
            {
                Width = (contentSizeConstraint.Width - (sizeOfContentSizedElements.Width + GapsSum)) / FillElementsCount
            };
        }

        return contentSizeConstraint with
        {
            Height = (contentSizeConstraint.Height - (sizeOfContentSizedElements.Height + GapsSum)) / FillElementsCount
        };
    }

    private int FillElementsCount => childLayoutBuilders.Length - _contentSizedElements.Count;

    private IEnumerable<ILayout> BuildChildLayouts(IEnumerable<ILayoutBuilder> layoutBuilders, Rectangle contentBounds, Size fillElementsSizeConstraint)
    {
        bool first = true;
        foreach (ILayoutBuilder childLayoutBuilder in layoutBuilders)
        {
            if (!first)
            {
                if (stackContainer.Direction == Direction.Horizontal)
                {
                    contentBounds = new Rectangle
                    (
                        Point: contentBounds.Point with
                        {
                            X = contentBounds.Point.X + stackContainer.Gap
                        },
                        Size: contentBounds.Size with
                        {
                            Width = contentBounds.Size.Width - stackContainer.Gap
                        }
                    );
                }
                else
                {
                    contentBounds = new Rectangle
                    (
                        Point: contentBounds.Point with
                        {
                            Y = contentBounds.Point.Y + stackContainer.Gap
                        },
                        Size: contentBounds.Size with
                        {
                            Height = contentBounds.Size.Height - stackContainer.Gap
                        }
                    );
                }
            }
            else
            {
                first = false;
            }
            
            Point startOfNextChild = contentBounds.Point;

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
            else // Horizontal
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
                        Width = Math.Min(fillElementsSizeConstraint.Width, contentBounds.Size.Width)
                    };
                }
                else
                {
                    childOuterSize = childOuterSize with
                    {
                        Height = Math.Min(fillElementsSizeConstraint.Height, contentBounds.Size.Height)
                    };
                }
            }
            else
            {
                childOuterSize = childOuterSize with
                {
                    Width = Math.Min(childOuterSize.Width, contentBounds.Size.Width)
                };
            }

            var childBounds = new Rectangle(startOfNextChild, childOuterSize);
            ILayout childLayout = childLayoutBuilder.BuildLayout(childBounds);
            yield return childLayout;

            if (stackContainer.Direction == Direction.Horizontal)
            {
                contentBounds = new Rectangle
                (
                    Point: contentBounds.Point with
                    {
                        X = childLayout.OuterBounds.Right
                    }, 
                    Size: contentBounds.Size with
                    {
                        Width = contentBounds.Size.Width - childLayout.OuterBounds.Size.Width
                    }
                );
            }
            else
            {
                contentBounds = new Rectangle
                (
                    Point: contentBounds.Point with
                    {
                        Y = childLayout.OuterBounds.Bottom
                    },
                    Size: contentBounds.Size with
                    {
                        Height = contentBounds.Size.Height - childLayout.OuterBounds.Size.Height
                    }
                );
            }
        }
    }
}