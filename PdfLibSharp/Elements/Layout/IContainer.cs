namespace PdfLibSharp.Elements.Layout;

public interface IContainer : IBorderElement, IElementAlignment, IFont, IStringFormat
{
    Pdf Pdf { get; }
    IReadOnlyList<IElement> Elements { get; }
    void Add(IElement element);
}