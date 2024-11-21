using PdfLibSharp.Drawing;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Layout;

internal record LineContentModel(Size Size, Direction Direction, Pen Pen) : ContentModel(Size);