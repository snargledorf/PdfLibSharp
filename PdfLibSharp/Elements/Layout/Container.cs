using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements.Layout;

internal abstract class Container : BaseBorderElement, IContainer
{
    private readonly List<IElement> _elements = [];
    
    public IReadOnlyList<IElement> Elements => _elements.AsReadOnly();

    public void Add(IElement element) => _elements.Add(element);

    public string? FontFamily { get; set; }
    public double? FontSize { get; set; }
    public FontStyles? FontStyles { get; set; }
    public Color? FontColor { get; set; }
    public double? LineHeight { get; set; }

    public ElementAlignment ElementAlignment { get; set; }
    
    public StringFormat? StringFormat { get; set; }
}