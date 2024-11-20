namespace PdfLibSharp.Layout;

internal interface IContainerLayout : ILayout
{
    IReadOnlyList<ILayout> ChildLayouts { get; }
}