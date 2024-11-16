using PdfLibrary.Elements.Layout;

namespace PdfLibrary.Elements;

public interface IPage : IStackContainer
{
    Pdf Pdf { get; }
}