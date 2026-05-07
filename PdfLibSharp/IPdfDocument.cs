using PdfLibSharp.Elements;

namespace PdfLibSharp;

public interface IPdfDocument : IDisposable, IAsyncDisposable
{
    IPdfPage AddPage(PageSize size);
    Task SaveAsync(CancellationToken cancellationToken = default);
}