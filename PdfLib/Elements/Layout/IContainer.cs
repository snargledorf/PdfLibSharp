namespace PdfLibrary.Elements.Layout;

public interface IContainer : IElement, IElementAlignment, IFont, IStringFormat
{
    IReadOnlyList<IElement> Elements { get; }
    void Add(IElement element);
}