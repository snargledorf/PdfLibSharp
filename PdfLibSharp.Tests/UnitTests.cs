using PdfLibSharp.Drawing;
using PdfLibSharp.Drawing.Units;

namespace PdfLibSharp.Tests;

public class UnitTests
{
    [Test]
    public void UnitsEqual()
    {
        Unit unitInch = Unit.Inch;
        Assert.Multiple(() =>
        {
            Assert.That(Unit.Inch, Is.EqualTo(unitInch));
            Assert.That(Unit.Millimeter, Is.Not.EqualTo(unitInch));
        });
    }

    [Test]
    public void PixelsForPpi()
    {
        UnitPixel unitPixel = Unit.Pixel;
        
        double points = unitPixel.ToPoints(1);
        
        Assert.That(points, Is.EqualTo(.75));
        
        unitPixel = UnitPixel.ForPpi(72);

        points = unitPixel.ToPoints(72);

        Assert.That(points, Is.EqualTo(72));
    }
}