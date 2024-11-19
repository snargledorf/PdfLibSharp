namespace PdfLibSharp.Elements.Content;

public interface ITextElement : IBorderElement, IFont, IStringFormat
{
    string Text { get; }
}