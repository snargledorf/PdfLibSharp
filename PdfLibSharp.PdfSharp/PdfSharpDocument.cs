using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PdfLibSharp;
using PdfLibSharp.Elements;
using PdfLibSharp.PdfSharp;
using PdfSharp.Pdf;

public class PdfSharpDocument(Stream stream) : IPdfDocument
{
    private readonly PdfDocument _pdfDocument = new();

    public IPdfPage AddPage(PageSize size)
    {
        PdfPage pdfPage = _pdfDocument.AddPage();
        pdfPage.Size = size.ToPdfSharpPageSize();
        return new PdfSharpPage(pdfPage);
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        return _pdfDocument.SaveAsync(stream);
    }

    public async ValueTask DisposeAsync()
    {
        _pdfDocument.Dispose();
        
        if (stream is IAsyncDisposable streamAsyncDisposable)
            await streamAsyncDisposable.DisposeAsync();
        else
            stream.Dispose();
        
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        stream.Dispose();
        _pdfDocument.Dispose();
        
        GC.SuppressFinalize(this);
    }
}