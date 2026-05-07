using System;
using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfSharp.Drawing;
using PdfSharpPageSize = PdfSharp.PageSize;

namespace PdfLibSharp.PdfSharp;

public static class SizeHelpers
{
    extension(Size size)
    {
        public XSize ToXSize() => new(size.Width, size.Height);
        public static Size FromXSize(XSize xSize) => new(xSize.Width, xSize.Height);
    }
    
    extension(XSize xSize)
    {
        public Size ToSize() => new(xSize.Width, xSize.Height);
    }

    extension(PageSize pageSize)
    {
        public PdfSharpPageSize ToPdfSharpPageSize()
        {
            if (pageSize == PageSize.A4)
                return PdfSharpPageSize.A4;
            
            if (pageSize == PageSize.Letter)
                return PdfSharpPageSize.Letter;
            
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, null);
        }
    }
}