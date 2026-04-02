using System.Text.Json;
using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Sdk.Grid;

public class Grid
{
    public List<Point[]> Points { get; private set; } = [];
    private readonly GridSettings _gridSettings;
    
    public Grid(CanvasSettings ctx, GridSettings gridSettings)
    {
        var cellWidth = gridSettings.CellWidth(ctx.DrawingAreaWidth);
        var cellHeight = gridSettings.CellHeight(ctx.DrawingAreaHeight);
        
        for (var yi = 0; yi < gridSettings.Height; yi++)
        {
            var rowPoints = new List<Point>();
            for (var xi = 0; xi < gridSettings.Width; xi++)
            {
                var newPointX = xi * cellWidth + ctx.LeftMargin;
                var newPointY = yi * cellHeight + ctx.TopMargin;
                rowPoints.Add(new Point(newPointX, newPointY));
            }
            Points.Add(rowPoints.ToArray());
        }
        
        _gridSettings = gridSettings;
    }

    public void Shake(double diff)
    {
        var result = new List<Point[]>();
        var smallDif = diff - 2.0;

        for (var r = 0; r < Points.Count; r++)
        {
            var shakenRow = new List<Point>();
            for (var c = 0; c < Points[r].Length; c++)
            {
                if (_gridSettings.FixedBorder && (c == 0 || c == Points[r].Length - 1 || r == 0 || r == Points.Count - 1))
                {
                    shakenRow.Add(new Point(Points[r][c].X, Points[r][c].Y));
                    continue;
                }
                var currentDiff = diff;
                if (c == 0 || c == Points[r].Length - 1)
                {
                    currentDiff = smallDif;
                }
                var xShaken = Random.Shared.NextDouble() * 2 * currentDiff - currentDiff;
                var yShaken = Random.Shared.NextDouble() * 2 * currentDiff - currentDiff;
                shakenRow.Add(new Point(
                    Math.Round(Points[r][c].X + xShaken, 3), 
                    Math.Round(Points[r][c].Y + yShaken, 3)));
            }
            result.Add(shakenRow.ToArray());
        }

        if (_gridSettings.SaveGrid)
        {
            var points = JsonSerializer.Serialize(result);
            File.WriteAllText("grid.json", points);
        }
        
        Points = result;
    }
}