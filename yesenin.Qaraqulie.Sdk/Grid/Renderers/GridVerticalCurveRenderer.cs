using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using Path = yesenin.Qaraqulie.Library.Path;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

public class GridVerticalCurveRenderer : IGridRenderer
{
    public DrawingGroup Render(Grid grid, GridSettings settings)
    {
        var g = new DrawingGroup("black", 0.5f);
        
        for (var c = 0; c < settings.GridWidth; c++)
        {
            if (c < settings.GridWidth - 1)
            {
                for (var p = 0; p < settings.Parts; p++)
                {
                    var linePoints = new List<Point>();
                    for (var r = 0; r < settings.GridHeight; r++)
                    {
                        var stepY = (grid.Points[r][c + 1].Y - grid.Points[r][c].Y) / settings.Parts;
                        var stepX = (grid.Points[r][c + 1].X - grid.Points[r][c].X) / settings.Parts;

                        var nextPoint = new Point(
                            grid.Points[r][c].X + p * stepX,
                            grid.Points[r][c].Y + p * stepY);
                        
                        linePoints.Add(nextPoint);
                    }

                    var path = new Path();
                    GridCurvePathBuilder.AddSmoothPath(path, linePoints);
                    g.AddItem(path);
                }
            }
            else
            {
                var linePoints = new List<Point>();
                for (var r = 0; r < settings.GridHeight; r++)
                {
                    linePoints.Add(grid.Points[r][c]);
                }
                var path = new Path();
                GridCurvePathBuilder.AddSmoothPath(path, linePoints);
                g.AddItem(path);
            }
        }

        return g;
    }
}
