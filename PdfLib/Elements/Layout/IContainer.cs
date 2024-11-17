namespace PdfLib.Elements.Layout;

public interface IContainer : IBorderElement, IElementAlignment, IFont, IStringFormat
{
    IReadOnlyList<IElement> Elements { get; }
    void Add(IElement element);
}