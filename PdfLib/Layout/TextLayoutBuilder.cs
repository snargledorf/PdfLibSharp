using PdfLibrary.Drawing;
using PdfLibrary.Elements.Content;

namespace PdfLibrary.Layout;

internal class TextLayoutBuilder(ITextElement textElement, Size contentSize, Font font, StringFormat stringFormat)
    : BorderLayoutBuilder(textElement, contentSize)
{
    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        return new TextLayout(
            textElement.Text,
            bounds.Point,
            ContentSize,
            textElement.Margins,
            textElement.GetFont(font),
            textElement.StringFormat ?? stringFormat,
            textElement.Brush,
            borderPen);
    }
}