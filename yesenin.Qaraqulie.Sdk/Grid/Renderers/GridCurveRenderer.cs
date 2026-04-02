using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using Path = yesenin.Qaraqulie.Library.Path;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

public class GridCurveRenderer : IGridRenderer
{
    public DrawingGroup Render(Grid grid, GridSettings settings)
    {
        var g = new DrawingGroup("black", 0.5f);
        for (var r = 0; r < grid.Points.Count; r++)
        {
            if (r < grid.Points.Count - 1)
            {
                
                for (var p = 0; p < settings.Parts; p++)
                {
                    var linePoints = new List<Point>();
                    for (var c = 0; c < grid.Points[r].Length; c++)
                    {
                        var stepY = (grid.Points[r + 1][c].Y - grid.Points[r][c].Y) / settings.Parts;
                        var stepX = (grid.Points[r + 1][c].X - grid.Points[r][c].X) / settings.Parts;

                        var nextPoint = new Point(
                            grid.Points[r][c].X + p * stepX,
                            grid.Points[r][c].Y + p * stepY);
                        
                        linePoints.Add(nextPoint);
                    }

                    var path = new Path();
                    path.AddLineTo(linePoints[0]);
                    for (var i = 1; i < linePoints.Count - 1; i++)
                    {
                        var moveLeft = linePoints[i].MoveTo(linePoints[i - 1], 3);
                        var moveRight = linePoints[i].MoveTo(linePoints[i + 1], 3);
                        path.AddCurve(moveLeft, linePoints[i], moveRight);
                    }
                    path.AddLineTo(linePoints[^1]);
                    g.AddItem(path);
                }
            }
            else
            {
                var linePoints = new List<Point>();
                for (var c = 0; c < grid.Points[r].Length; c++)
                {
                    linePoints.Add(grid.Points[r][c]);
                }
                var path = new Path();
                path.AddLineTo(linePoints[0]);
                for (var i = 1; i < linePoints.Count - 1; i++)
                {
                    var moveLeft = linePoints[i].MoveTo(linePoints[i - 1], 3);
                    var moveRight = linePoints[i].MoveTo(linePoints[i + 1], 3);
                    path.AddCurve(moveLeft, linePoints[i], moveRight);
                }
                path.AddLineTo(linePoints[^1]);
                g.AddItem(path);
            }
        }

        return g;
    }
}