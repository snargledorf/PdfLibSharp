namespace PdfLibSharp.Layout;

internal static class PositionedElementExtensions
{
    public static T ContentAs<T>(this PositionedLayout positionedLayout)
    {
        return (T)positionedLayout.Content;
    }
}