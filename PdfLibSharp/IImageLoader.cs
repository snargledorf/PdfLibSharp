using PdfLibSharp.Drawing;

namespace PdfLibSharp;

public interface IImageLoader
{
    IImage LoadImageFromFile(string filePath);
    IImage LoadImageFromStream(Stream stream);
}