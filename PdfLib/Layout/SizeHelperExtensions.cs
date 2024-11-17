using PdfLib.Drawing;
using PdfLib.Elements.Layout;

namespace PdfLib.Layout;

public static class SizeHelperExtensions
{
    public static Size GetCombinedSize(this IEnumerable<Size> elementSizes, Direction direction)
    {
        IEnumerable<Size> enumerable = elementSizes as Size[] ?? elementSizes.ToArray();
        
        if (!enumerable.Any())
            return new Size();
        
        return enumerable.Aggregate((current, next) =>
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
    }
}