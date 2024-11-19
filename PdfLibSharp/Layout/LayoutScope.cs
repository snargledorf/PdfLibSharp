using PdfLibSharp.Drawing;

namespace PdfLibSharp.Layout;

internal record LayoutScope(Font Font, double LineHeight, StringFormat StringFormat, Color FontColor);