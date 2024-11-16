namespace PdfLib.Elements;

public interface IFont
{
    string? FontFamily { get; set; }
    double? FontSize { get; set; }
    FontStyles? FontStyles { get; set; }
}