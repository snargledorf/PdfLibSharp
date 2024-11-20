using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

public static class SizeHelperExtensions
{
    public static Size GetCombinedSize(this IEnumerable<Size> elementSizes, Direction direction)
    {
        return GetCombinedSize(elementSizes, direction, 0);
    }

    public static Size GetCombinedSize(this IEnumerable<Size> elementSizes, Direction direction, Dimension gap)
    {
        IReadOnlyCollection<Size> sizes = elementSizes as Size[] ?? elementSizes.ToArray();
        
        if (!sizes.Any())
            return new Size();
        
        Size totalSize = sizes.Aggregate((current, next) =>
        {
            return direction switch
            {
                Direction.Horizontal => new Size
                (
                    Width: current.Width + next.Width,
                    Height: Math.Max(current.Height, next.Height)
                ),
                Direction.Vertical => new Size
                (
                    Height: current.Height + next.Height,
                    Width: Math.Max(current.Width, next.Width)
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };
        });

        Dimension gapSum = gap * (sizes.Count - 1);
        if (gapSum <= 0)
            return totalSize;
        
        if (direction == Direction.Horizontal)
        {
            return totalSize with
            {
                Width = totalSize.Width + gapSum
            };
        }
        
        return totalSize with
        {
            Height = totalSize.Height + gapSum
        };
    }
}