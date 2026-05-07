namespace PdfLibSharp.Drawing;

public interface IGraphics : IMeasureGraphics
{
    static IImageLoader ImageLoader { get; }
    
    void DrawLine(Pen pen, Point start, Point end);
    void DrawString(string value, Font font, Brush brush, Rectangle rect, StringFormat format);
    void DrawImage(IImage image, Rectangle rect);
    void DrawRectangle(Pen pen, Rectangle rect);
}