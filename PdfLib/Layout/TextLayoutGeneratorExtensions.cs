using PdfLibrary.Drawing;
using PdfLibrary.Elements.Content;

namespace PdfLibrary.Layout;

internal static class TextLayoutGeneratorExtensions
{
    internal static ILayoutBuilder GetLayoutBuilder(this ITextElement textElement, Font font,
        StringFormat stringFormat, IMeasureGraphics measureGraphics)
    {
        Size contentSize = textElement.GetContentSize(font, measureGraphics);
        return new TextLayoutBuilder(textElement, contentSize, font, stringFormat);
    }

    internal static Size GetContentSize(this ITextElement textElement, Font font, IMeasureGraphics measureGraphics)
    {
        return textElement.GetSize(measureGraphics.MeasureString(textElement.Text, font));
    }
}