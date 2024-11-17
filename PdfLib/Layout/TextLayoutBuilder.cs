using System.Text;
using PdfLib.Drawing;
using PdfLib.Elements.Content;

namespace PdfLib.Layout;

internal class TextLayoutBuilder(ITextElement textElement, IReadOnlyCollection<Line> lines, Size contentSize, Font font, StringFormat stringFormat)
    : BorderLayoutBuilder(textElement, contentSize)
{
    protected override ILayout BuildLayout(Rectangle bounds, Pen? borderPen)
    {
        return new TextLayout(
            lines,
            bounds.Point,
            ContentSize,
            textElement.Margins,
            textElement.GetFont(font),
            textElement.StringFormat ?? stringFormat,
            textElement.Brush,
            borderPen);
    }
}

internal record Word(string Text, Size Size);

internal record Line(string LineText, Rectangle ContentBounds);