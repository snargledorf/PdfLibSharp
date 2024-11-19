using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp.Tests;

public class UnitTests
{
    [Test]
    public void PixelsForPpi()
    {
        UnitPixel unitPixel = UnitPixel.ForPpi();
        
        double points = unitPixel.ToPoints(1);
        
        Assert.That(points, Is.EqualTo(.75));
        
        unitPixel = UnitPixel.ForPpi(72);

        points = unitPixel.ToPoints(72);

        Assert.That(points, Is.EqualTo(72));
    }
}