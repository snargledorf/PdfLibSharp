using PdfLib.Drawing;

namespace PdfLib.Elements.Content;

internal class TextElement(string text) : BaseBorderElement, ITextElement
{
    public string Text { get; } = text;
    public string? FontFamily { get; set; }
    public double? FontSize { get; set; }
    public FontStyles? FontStyles { get; set; }
    public Color? FontColor { get; set; }
    public double? LineHeight { get; set; }
    public StringFormat? StringFormat { get; set; }
    public Brush Brush { get; set; } = new SolidBrush(Color.Black);
}