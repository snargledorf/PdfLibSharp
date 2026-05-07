using PdfSharp.Fonts;

namespace PdfLibSharp.Tests.PdfSharp;

[SetUpFixture]
public class FontSetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = true;
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        GlobalFontSettings.UseWindowsFontsUnderWindows = false;
    }
}