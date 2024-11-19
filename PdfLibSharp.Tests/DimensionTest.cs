using PdfLibSharp.Drawing;

namespace PdfLibSharp.Tests;

public class DimensionTest
{
    [Test]
    public void DimensionsEqual()
    {
        var dim1 = new Dimension();
        var dim2 = new Dimension();
        var dim3 = new Dimension(0, Unit.Inch);
        
        Assert.That(dim1, Is.EqualTo(dim2));
        Assert.That(dim1, Is.EqualTo(dim3));
    }
}