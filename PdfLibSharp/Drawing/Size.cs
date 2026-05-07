namespace PdfLibSharp.Drawing;

public readonly record struct Size(Dimension Width, Dimension Height)
{
    public static Size operator +(Size a, Size b) => new(a.Width + b.Width, a.Height + b.Height);
    public static Size operator -(Size a, Size b) => new(a.Width - b.Width, a.Height - b.Height);

    public static Size operator /(Size size, int divisor) => new(size.Width / divisor, size.Height / divisor);
    
    public static Size operator -(Size size, Dimension dimension) => new(size.Width - dimension, size.Height - dimension);
    
    public static Size operator +(Size size, Dimension dimension) => new(size.Width + dimension, size.Height + dimension);
}