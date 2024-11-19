using PdfSharp.Drawing;

namespace PdfLibSharp.Drawing;


public readonly record struct Rectangle(Point Point, Size Size)
{
    public static implicit operator XRect(Rectangle rectangle) => new(rectangle.Point, rectangle.Size);
    
    public Dimension Bottom => Point.Y + Size.Height;
    public Point TopLeft => Point;
    public Point BottomLeft => Point with { Y = Point.Y + Size.Height };
    public Dimension Right => Point.X + Size.Width;
}