using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Content;

namespace PdfLibSharp.Layout;

internal static class TextElementExtensions
{
    internal static ILayoutFactory CreateLayoutFactory(this ITextElement textElement, LayoutScope scope, IMeasureGraphics measureGraphics)
    {
        return new TextLayoutFactory(textElement, scope, measureGraphics);
    }
}