using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal class StackLayoutModelFactory(ILayoutModelFactoryProvider layoutModelFactoryProvider)
    : BorderLayoutModelFactory
{
    protected override ContentModel CreateContentModel(IElement element, Size constraints, LayoutScope scope)
    {
        if (element is not StackContainer stackContainer)
            throw new ArgumentException("Element must be a StackContainer", nameof(element));
        
        scope = new LayoutScope
        (
            stackContainer.GetFont(scope.Font),
            stackContainer.LineHeight ?? scope.LineHeight,
            stackContainer.StringFormat ?? scope.StringFormat,
            stackContainer.FontColor ?? scope.FontColor
        );
        
        return base.CreateContentModel(element, constraints, scope);
    }

    protected override ContentModel CreateBorderedContentModel(IBorderElement borderElement, Pen? borderPen, Size constraints,
        LayoutScope scope)
    {
        if (borderElement is not StackContainer stackContainer)
            throw new ArgumentException("Element must be a StackContainer", nameof(borderElement));

        Size fillElementsSizeConstraint = GetFillElementsSizeConstraint(
            stackContainer.Elements,
            constraints,
            stackContainer.Direction,
            stackContainer.Gap,
            scope
        );

        LayoutModel[] childLayouts = BuildLayoutModels(
            stackContainer.Elements,
            constraints,
            fillElementsSizeConstraint,
            stackContainer.Direction,
            stackContainer.Gap,
            scope
        ).ToArray();

        Size contentSize = childLayouts
            .Select(layout => layout.ContentModel.Size + layout.Margins.ToSize())
            .GetCombinedSize(stackContainer.Direction, stackContainer.Gap);

        return new StackRenderModel(stackContainer, stackContainer.GetSize(contentSize), borderPen, childLayouts);
    }

    private Size GetFillElementsSizeConstraint(IReadOnlyList<IElement> childElements, Size constraints,
        Direction direction, Dimension gap, LayoutScope scope)
    {
        IEnumerable<IElement> contentSizedElements =
            childElements.Where(factory => factory.Sizing == ElementSizing.Content);
        
        IReadOnlyList<LayoutModel> contentSizedLayouts =
            BuildLayoutModels(contentSizedElements, constraints, new Size(), direction, gap, scope).ToArray();

        int fillElementsCount = childElements.Count(element => element.Sizing == ElementSizing.ExpandToFillBounds);
        
        return StackLayoutUtilities.CalculateFillElementsSizeConstraint(
            contentSizedLayouts,
            fillElementsCount,
            constraints,
            direction,
            gap
        );
    }

    private IEnumerable<LayoutModel> BuildLayoutModels(IEnumerable<IElement> elements,
        Size constraints,
        Size fillElementsSizeConstraint,
        Direction direction,
        Dimension gap, 
        LayoutScope scope)
    {
        LayoutModel? previousLayout = null;
        foreach (IElement element in elements)
        {
            ILayoutModelFactory elementLayoutModelFactory = layoutModelFactoryProvider.GetFactory(element.GetType());
            
            if (previousLayout is not null)
            {
                Size previousLayoutOuterSize = previousLayout.ContentModel.Size + previousLayout.Margins.ToSize();

                constraints = StackLayoutUtilities.UpdateConstraints(
                    constraints,
                    previousLayoutOuterSize,
                    direction,
                    gap
                );
            }

            Size elementConstraints = StackLayoutUtilities.UpdateConstraintsForElementSizing(
                constraints,
                element.Sizing,
                fillElementsSizeConstraint,
                constraints,
                direction
            );

            previousLayout = elementLayoutModelFactory.CreateLayoutModel(element, elementConstraints, scope);
            yield return previousLayout;
        }
    }
}