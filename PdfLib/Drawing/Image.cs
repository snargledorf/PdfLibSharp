using PdfSharp.Drawing;

namespace PdfLibrary.Drawing;

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
        return new Image(XImage.FromFile(filePath));
    }

    public static implicit operator XImage(Image image) => image._image;
}