using PdfLibSharp.Elements;
using SkiaSharp;

namespace PdfLibSharp.Skia;

public class SkiaPdfDocument(SKDocument skDocument, Stream stream, int ppi) : IPdfDocument
{
    public IPdfPage AddPage(PageSize size)
    {
        return new SkiaPage(size.Size, skDocument, ppi);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        skDocument.Close();
        await stream.FlushAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        skDocument.Close();
        skDocument.Dispose();
        await stream.DisposeAsync();
        
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        skDocument.Close();
        skDocument.Dispose();
        stream.Dispose();
        
        GC.SuppressFinalize(this);
    }
}