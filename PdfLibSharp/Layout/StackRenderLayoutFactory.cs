using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal sealed class StackRenderLayoutFactory(IRenderLayoutFactoryProvider renderLayoutFactoryProvider) : BaseRenderLayoutFactory
{
    protected override Content BuildContent(ContentModel contentModel, Rectangle contentBounds)
    {
        if (contentModel is not StackRenderModel stackContentModel)
            throw new ArgumentException("ContentModel must be a StackContentModel", nameof(contentModel));
        
        Size fillElementsSizeConstraint = StackLayoutUtilities.CalculateFillElementsSizeConstraint(
            stackContentModel.ContentSizedChildModels,
            stackContentModel.FillElementsCount,
            contentBounds.Size,
            stackContentModel.StackContainer.Direction,
            stackContentModel.StackContainer.Gap
        );
        
        var childPositionedLayouts = new List<RenderLayout>();
        
        Rectangle currentContentBounds = contentBounds;

        RenderLayout? previousLayout = null;
        foreach (LayoutModel childLayoutModel in stackContentModel.ChildLayoutModels)
        {
            if (previousLayout is not null)
            {
                currentContentBounds = currentContentBounds with
                {
                    Size = StackLayoutUtilities.UpdateConstraints(
                        currentContentBounds.Size,
                        previousLayout.OuterBounds.Size,
                        stackContentModel.StackContainer.Direction,
                        stackContentModel.StackContainer.Gap
                    )
                };
                
                if (stackContentModel.StackContainer.Direction == Direction.Horizontal)
                {
                    currentContentBounds = currentContentBounds with
                    {
                        Point = currentContentBounds.Point with
                        {
                            X = previousLayout.OuterBounds.Right + stackContentModel.StackContainer.Gap
                        }
                    };
                }
                else
                {
                    currentContentBounds = currentContentBounds with
                    {
                        Point = currentContentBounds.Point with
                        {
                            Y = previousLayout.OuterBounds.Bottom + stackContentModel.StackContainer.Gap
                        }
                    };
                }
            }

            Size childOuterSize = DetermineChildSizeOuterSize(
                childLayoutModel.ContentModel.Size + childLayoutModel.Margins.ToSize(),
                fillElementsSizeConstraint,
                currentContentBounds.Size,
                childLayoutModel.Sizing,
                stackContentModel.StackContainer.ElementAlignment,
                stackContentModel.StackContainer.Direction
            );

            Point childPoint = DetermineChildPoint(
                currentContentBounds,
                childOuterSize,
                stackContentModel.StackContainer.ElementAlignment,
                stackContentModel.StackContainer.Direction
            );

            var childBounds = new Rectangle(childPoint, childOuterSize);

            IRenderLayoutFactory renderLayoutFactory = renderLayoutFactoryProvider.GetFactory(childLayoutModel.ContentModel.GetType());
            previousLayout = renderLayoutFactory.CreateRenderLayout(childLayoutModel, childBounds);
            childPositionedLayouts.Add(previousLayout);
        }

        return new ContainerContent(contentBounds, stackContentModel.BorderPen, childPositionedLayouts.ToArray());
    }

    private static Size DetermineChildSizeOuterSize(
        Size childCurrentOuterSize,
        Size fillElementsSizeConstraint,
        Size constraints,
        ElementSizing elementSizing,
        ElementAlignment elementAlignment, 
        Direction direction)
    {
        if (elementAlignment == ElementAlignment.Stretch)
        {
            if (direction == Direction.Vertical)
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
            direction
        );
    }

    private static Point DetermineChildPoint(
        Rectangle bounds, 
        Size childOuterSize,
        ElementAlignment elementAlignment, 
        Direction direction)
    {
        if (direction == Direction.Vertical)
        {
            switch (elementAlignment)
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
            switch (elementAlignment)
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