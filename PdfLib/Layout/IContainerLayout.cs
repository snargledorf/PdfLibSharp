namespace PdfLibrary.Layout;

internal interface IContainerLayout : ILayout
{
    IReadOnlyList<ILayout> Children { get; }
}