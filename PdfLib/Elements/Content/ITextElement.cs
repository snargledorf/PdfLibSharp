namespace PdfLibrary.Elements.Content;

public interface ITextElement : IElement, IFont, IBrush, IStringFormat
{
    string Text { get; }
}