using System;
using PdfLibSharp.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfLibSharp.PdfSharp;

public sealed class Graphics : IGraphics
{
    private readonly XGraphics _backingGraphics;

    private Graphics(XGraphics backingGraphics)
    {
        _backingGraphics = backingGraphics;
    }
    
    public static IMeasureGraphics ForMeasure(XSize size)
    {
        return new MeasureGraphics(XGraphics.CreateMeasureContext(size, XGraphicsUnit.Point,
            XPageDirection.Downwards));
    }

    public static IGraphics FromPdfPage(PdfPage pdfPage)
    {
        return new Graphics(XGraphics.FromPdfPage(pdfPage));
    }

    public Size MeasureString(string text, Font font)
    {
        return _backingGraphics.MeasureString(text, font.ToXFont()).ToSize();
    }

    public void DrawImage(Image image, Rectangle rect)
    {
        if (image is not IPdfSharpImage pdfSharpImage)
            throw new ArgumentException("Only PdfSharpImage is supported for drawing");
        
        _backingGraphics.DrawImage(pdfSharpImage.Image, rect.ToXRect());
    }

    public void DrawString(string value, Font font, Brush brush, Rectangle rect, StringFormat format)
    {
        _backingGraphics.DrawString(value, font.ToXFont(), brush.ToXBrush(), rect.ToXRect(), format.ToXStringFormat());
    }

    public void DrawLine(Pen pen, Point start, Point end)
    {
        _backingGraphics.DrawLine(pen.ToXPen(), start.ToXPoint(), end.ToXPoint());
    }

    public void DrawRectangle(Pen pen, Rectangle rect)
    {
        _backingGraphics.DrawRectangle(pen.ToXPen(), rect.ToXRect());
    }

    public void Dispose()
    {
        _backingGraphics.Dispose();
    }

    public static implicit operator Graphics(XGraphics graphics) => new(graphics);

    private class MeasureGraphics(Graphics graphics) : IMeasureGraphics
    {
        public Size MeasureString(string text, Font font)
        {
            return graphics.MeasureString(text, font);
        }

        public void Dispose()
        {
            graphics.Dispose();
        }
    }
}