namespace PdfLib.Elements.Content;

public interface ITextElement : IBorderElement, IFont, IBrush, IStringFormat
{
    string Text { get; }
}