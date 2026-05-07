using System.IO;
using PdfLibSharp.Drawing;
using PdfSharp.Drawing;

namespace PdfLibSharp.PdfSharp;

public class PdfSharpImageLoader : IImageLoader
{
    private PdfSharpImageLoader()
    {    
    }
    
    public static PdfSharpImageLoader Instance { get; } = new();

    public IImage LoadImageFromFile(string filePath)
    {
        return new PdfSharpImage(XImage.FromFile(filePath));
    }

    public IImage LoadImageFromStream(Stream stream)
    {
        return new PdfSharpImage(XImage.FromStream(stream));
    }
}