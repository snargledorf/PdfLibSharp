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
}