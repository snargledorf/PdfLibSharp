using PdfLibSharp.Drawing;

namespace PdfLibSharp.Elements;

public interface IFont
{
    string? FontFamily { get; set; }
    double? FontSize { get; set; }
    FontStyles? FontStyles { get; set; }
    Color? FontColor { get; set; }
    double? LineHeight { get; set; }
}