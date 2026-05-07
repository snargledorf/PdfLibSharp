using PdfLibSharp.Drawing;
using PdfLibSharp.PdfSharp;

namespace PdfLibSharp.Tests;

public class ImageTests
{
    [Test]
    public void PdfSharp_ImageFromStream()
    {
        byte[] imageBytes = Convert.FromBase64String(Resources.PdfIcon);
        using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length, false, true);

        Image image = PdfSharpImageLoader.Instance.LoadImageFromStream(ms);
        Assert.That(image, Is.Not.Null);
        
        (Dimension width, Dimension height) = image.Size;
        Dimension expectedWidthHeight = Dimension.FromPixels(512);

        Assert.Multiple(() =>
        {
            Assert.That(double.Round(width), Is.EqualTo(expectedWidthHeight.Points));
            Assert.That(double.Round(height), Is.EqualTo(expectedWidthHeight.Points));
        });
    }
}