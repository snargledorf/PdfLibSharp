using PdfLibSharp.Elements;

namespace PdfLibSharp.Drawing;

public record Font(string Family, double Size, FontStyles FontStyles = FontStyles.Normal);