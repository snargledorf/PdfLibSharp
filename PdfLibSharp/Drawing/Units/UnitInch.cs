namespace PdfLibSharp.Drawing.Units;

public sealed class UnitInch : Unit
{
    public const double PointsPerInch = 72;
    
    public override double PointsPerUnit => PointsPerInch;
}