
using PdfLibrary.Drawing;

namespace PdfLibrary.Elements.Layout;

internal abstract class Container : BaseElement, IContainer
{
    private readonly List<IElement> _elements = [];
    
    public IReadOnlyList<IElement> Elements => _elements.AsReadOnly();

    public void Add(IElement element) => _elements.Add(element);

    public string? FontFamily { get; set; }

    public double? FontSize { get; set; }

    public FontStyles? FontStyles { get; set; }

    public ElementAlignment ElementAlignment { get; set; }
    
    public StringFormat? StringFormat { get; set; }
}