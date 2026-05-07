using PdfLibSharp.Drawing;
using PdfLibSharp.Drawing.Units;
using SkiaSharp;

namespace PdfLibSharp.Skia;

public sealed class Graphics : IGraphics
{
    private readonly SKCanvas _canvas;
    private readonly UnitPixel _unitPixel;

    internal Graphics(SKCanvas canvas, int ppi)
    {
        _canvas = canvas;
        _unitPixel = UnitPixel.ForPpi(ppi);
    }

    public void Dispose()
    {
        _canvas.Dispose();
    }
    
    public Size MeasureString(string text, Font font)
    {
        var skFont = new SKFont()
        {
            Size = (float)font.Size,
            Typeface = SKTypeface.FromFamilyName(font.Family),
        };
        
        _ = skFont.MeasureText(text, out SKRect rect);
        return new Size(new Dimension(rect.Width, _unitPixel), new Dimension(rect.Height, _unitPixel));
    }

    public void DrawLine(Pen pen, Point start, Point end)
    {
        var skPaint = new SKPaint()
        {
            Color = new SKColor(pen.Color.Argb),
            StrokeWidth = (float)pen.Width.ToPixels(_unitPixel),
        };
        
        _canvas.DrawLine((float)start.X.ToPixels(_unitPixel), (float)start.Y.ToPixels(_unitPixel), (float)end.X.ToPixels(_unitPixel), (float)end.Y.ToPixels(_unitPixel), skPaint);
    }

    public void DrawString(string value, Font font, Brush brush, Rectangle rect, StringFormat format)
    {
        throw new NotImplementedException();
    }

    public void DrawImage(Image image, Rectangle rect)
    {
        throw new NotImplementedException();
    }

    public void DrawRectangle(Pen pen, Rectangle rect)
    {
        throw new NotImplementedException();
    }
}