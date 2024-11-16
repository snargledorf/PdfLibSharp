using PdfSharp.Drawing;

namespace PdfLibrary.Drawing;

public readonly record struct Point(Dimension X, Dimension Y)
{
    public static readonly Point Zero = new(0, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public static implicit operator XPoint(Point p) => new(p.X, p.Y);
    
    public static implicit operator Point(XPoint p) => new(p.X, p.Y);
}