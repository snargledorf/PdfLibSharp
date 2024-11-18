using PdfLib.Drawing;

namespace PdfLib.Layout;

internal record LayoutScope(Font Font, double LineHeight, StringFormat StringFormat, Color FontColor);