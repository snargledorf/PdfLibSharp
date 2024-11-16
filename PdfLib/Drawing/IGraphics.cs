namespace PdfLib.Drawing;

internal interface IGraphics : IMeasureGraphics
{
    void DrawLine(Pen pen, Point start, Point end);
    void DrawString(string value, Font font, Brush brush, Rectangle rect, StringFormat format);
    void DrawImage(Image image, Rectangle rect);
    void DrawRectangle(Pen pen, Rectangle rect);
}