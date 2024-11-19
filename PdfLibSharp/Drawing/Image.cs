using PdfSharp.Drawing;

namespace PdfLibSharp.Drawing;

public sealed class Image
{
    private readonly XImage _image;

    private Image(XImage image)
    {
        _image = image;
    }

    public Size Size => _image.Size;

    public static Image FromFile(string filePath)
    {
        return XImage.FromFile(filePath);
    }

    public static Image FromStream(Stream stream)
    {
        return XImage.FromStream(stream);
    }

    public static implicit operator XImage(Image image) => image._image;
    
    public static implicit operator Image(XImage image) => new(image);
}