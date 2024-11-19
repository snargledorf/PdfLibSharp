using PdfLibSharp.Drawing;

namespace PdfLibSharp.Tests;

public class SizeTests
{
    [Test]
    public void SizeEqual()
    {
        var actual = new Size();
        var expected = new Size();
        Assert.That(actual, Is.EqualTo(expected));
    }
}