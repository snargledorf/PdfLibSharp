namespace PdfLibSharp.Drawing;

public interface IImageLoader
{
    Image LoadImageFromFile(string filePath);
    Image LoadImageFromStream(Stream stream);
}