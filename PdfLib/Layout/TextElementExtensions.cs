using PdfLib.Drawing;
using PdfLib.Elements.Content;

namespace PdfLib.Layout;

internal static class TextElementExtensions
{
    internal static ILayoutBuilder GetLayoutBuilder(this ITextElement textElement, Font font,
        StringFormat stringFormat, IMeasureGraphics measureGraphics)
    {
        font = textElement.GetFont(font);
        stringFormat = textElement.StringFormat ?? stringFormat;
        
        Size contentSize = textElement.GetContentSize(font, measureGraphics);
        return new TextLayoutBuilder(textElement, contentSize, font, stringFormat);
    }

    internal static Size GetContentSize(this ITextElement textElement, Font font, IMeasureGraphics measureGraphics)
    {
        Size measureString = measureGraphics.MeasureString(textElement.Text, font);
        return textElement.GetSize(measureString);
    }
}