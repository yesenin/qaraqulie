namespace yesenin.Qaraqulie.Sdk.Grid;

public class GridSettings
{
    public required string Name { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int Parts { get; init; }
    public required double ShakeIntensity { get; init; }
    public bool SaveGrid {get; init;}
    public bool FixedBorder {get; init;}

    public double CellWidth(double allowedWidth) => allowedWidth / (Width - 1);
    public double CellHeight(double allowedHeight) => allowedHeight / (Height - 1);
}