using PdfLib.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PdfLib.Drawing;

internal sealed class Graphics : IGraphics
{
    private readonly XGraphics _backingGraphics;

    private Graphics(XGraphics backingGraphics)
    {
        _backingGraphics = backingGraphics;
    }

    public static IMeasureGraphics ForMeasure(Size size)
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
        return _backingGraphics.MeasureString(text, font);
    }

    public void DrawImage(Image image, Rectangle rect)
    {
        _backingGraphics.DrawImage(image, rect);
    }

    public void DrawString(string value, Font font, Brush brush, Rectangle rect, StringFormat format)
    {
        _backingGraphics.DrawString(value, font, brush, rect, format);
    }

    public void DrawLine(Pen pen, Point start, Point end)
    {
        _backingGraphics.DrawLine(pen, start, end);
    }

    public void DrawRectangle(Pen pen, Rectangle rect)
    {
        _backingGraphics.DrawRectangle(pen, rect);
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