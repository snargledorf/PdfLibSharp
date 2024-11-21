using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout
{
    internal static class StackLayoutUtilities
    {
        public static Size CalculateFillElementsSizeConstraint(IReadOnlyCollection<LayoutModel> contentSizedLayoutModels, int fillElementsCount, Size constraints, Direction direction, Dimension gap)
        {
            if (fillElementsCount == 0)
                return new Size();

            Size outerSizeOfContentLayoutsWithGapSum = contentSizedLayoutModels
                .Select(layout => layout.ContentModel.Size + layout.Margins.ToSize())
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