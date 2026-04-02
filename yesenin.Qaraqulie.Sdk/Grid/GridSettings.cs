namespace yesenin.Qaraqulie.Sdk.Grid;

public class GridSettings
{
    public required int GridWidth { get; init; }
    public required int GridHeight { get; init; }
    public required int Parts { get; init; }
    public required double ShakeIntensity { get; init; }
    
    public bool FixedBorder {get; init;}

    public double CellWidth(double allowedWidth) => allowedWidth / (GridWidth - 1);
    public double CellHeight(double allowedHeight) => allowedHeight / (GridHeight - 1);
}