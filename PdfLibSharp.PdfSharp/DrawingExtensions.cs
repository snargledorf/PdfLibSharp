using System;
using PdfLibSharp.Drawing;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public static class DrawingExtensions
{
    extension(Color color)
    {
        public XColor ToXColor() => XColor.FromArgb(color.Argb);
    }

    extension(Brush brush)
    {
        public XBrush ToXBrush()
        {
            return brush switch
            {
                SolidBrush solidBrush => new XSolidBrush(solidBrush.Color.ToXColor()),
                _ => throw new ArgumentException($"Unsupported brush type {brush.GetType().Name}", nameof(brush))
            };
        }
    }

    extension(Rectangle rectangle)
    {
        public XRect ToXRect() => new(rectangle.TopLeft.X, rectangle.TopLeft.Y, rectangle.Size.Width,
            rectangle.Size.Height);
    }

    extension(StringFormat stringFormat)
    {
        public XStringFormat ToXStringFormat() => new()
        {
            Alignment = (XStringAlignment)stringFormat.Alignment,
            LineAlignment = (XLineAlignment)stringFormat.LineAlignment
        };
    }

    extension(Pen pen)
    {
        public XPen ToXPen() => new(pen.Color.ToXColor(), pen.Width);
    }
    
    extension(Point point)
    {
        public XPoint ToXPoint() => new(point.X, point.Y);
    }
}