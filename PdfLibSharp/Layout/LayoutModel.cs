using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;

namespace PdfLibSharp.Layout;

internal record LayoutModel(ContentModel ContentModel, Margins Margins, ElementSizing Sizing, Dimension? Width, Dimension? Height);