using PdfSharp.Drawing;

namespace PdfLib.Elements.Content;

public enum LineAlignment
{
    Near = XLineAlignment.Near,
    Far = XLineAlignment.Far,
    Center = XLineAlignment.Center,
    Baseline = XLineAlignment.BaseLine
}