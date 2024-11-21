namespace PdfLibSharp.Layout;

internal static class RenderLayoutExtensions
{
    public static T ContentAs<T>(this RenderLayout renderLayout) where T : Content
    {
        return (T)renderLayout.Content;
    }
}