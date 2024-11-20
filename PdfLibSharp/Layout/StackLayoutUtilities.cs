using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout
{
    internal static class StackLayoutUtilities
    {
        public static Size CalculateFillElementsSizeConstraint(IReadOnlyCollection<ILayout> layouts, Size constraints, Direction direction, Dimension gap)
        {
            int fillElementsCount = layouts.Count(layout => layout.Sizing == ElementSizing.ExpandToFillBounds);
            
            if (fillElementsCount == 0)
                return new Size();

            IReadOnlyList<ILayout> contentSizedLayouts =
                layouts.Where(layout => layout.Sizing == ElementSizing.Content).ToArray();

            Size outerSizeOfContentLayoutsWithGapSum = contentSizedLayouts
                .Select(layout => layout.ContentSize + layout.Margins.ToSize())
                .GetCombinedSize(direction, gap);

            if (direction == Direction.Horizontal)
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

        public static Size UpdateConstraints(Size constraints, Size elementOuterSize, Direction direction, Dimension gap)
        {
            Size outerSizeWithGap = elementOuterSize + gap;
            
            if (direction == Direction.Horizontal)
            {
                return constraints with
                {
                    Width = constraints.Width - outerSizeWithGap.Width
                };
            }

            return constraints with
            {
                Height = constraints.Height - outerSizeWithGap.Height
            };
        }

        public static Size UpdateConstraintsForElementSizing(Size constraints, ElementSizing elementSizing, Size fillSizeConstraint, Size outerContraints, Direction direction)
        {
            if (elementSizing != ElementSizing.ExpandToFillBounds)
                return constraints;
        
            if (direction == Direction.Horizontal)
            {
                return constraints with
                {
                    Width = Math.Min(fillSizeConstraint.Width, outerContraints.Width)
                };
            }

            return constraints with
            {
                Height = Math.Min(fillSizeConstraint.Height, outerContraints.Height)
            };
        }
    }
}