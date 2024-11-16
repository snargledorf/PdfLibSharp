using PdfLib.Elements.Layout;

namespace PdfLib.Elements;

public interface IPage : IStackContainer
{
    Pdf Pdf { get; }
}