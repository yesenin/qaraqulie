using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

public class GridVerticalEdgeRenderer : IGridRenderer
{
    public DrawingGroup Render(Grid grid, GridSettings settings)
    {
        var g = new DrawingGroup("black", 0.5f);
        for (var c = 0; c < settings.Width; c++)
        {
            if (c < settings.Width - 1)
            {
                for (var p = 0; p < settings.Parts; p++)
                {
                    var polyline = new Polyline(0.3f, "black");
                    for (var r = 0; r < settings.Height; r++)
                    {
                        var stepY = (grid.Points[r][c + 1].Y - grid.Points[r][c].Y) / settings.Parts;
                        var stepX = (grid.Points[r][c + 1].X - grid.Points[r][c].X) / settings.Parts;
                        polyline.AddPoint(new Point(
                            grid.Points[r][c].X + p * stepX, 
                            grid.Points[r][c].Y + p * stepY)
                        );
                    }
                    g.AddItem(polyline);
                }
            }
            else
            {
                var polyline = new Polyline(0.3f, "black");
                for (var r = 0; r < settings.Height; r++)
                {
                    polyline.AddPoint(grid.Points[r][c]);
                }
                g.AddItem(polyline);
            }
        }

        return g;
    }
}