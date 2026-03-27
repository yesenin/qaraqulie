using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

public class GridEdgeRenderer : IGridRenderer
{
    public DrawingGroup Render(Grid grid, GridSettings settings)
    {
        var g = new DrawingGroup("black", 0.5f);
        var leftToRight = true;
        for (var r = 0; r < grid.Points.Count; r++)
        {
            if (r < grid.Points.Count - 1)
            {
                for (var p = 0; p < settings.Parts; p++)
                {
                    var polyline = new Polyline(0.3f, "black");
                    for (var c = 0; c < grid.Points[r].Length; c++)
                    {
                        var stepY = (grid.Points[r + 1][c].Y - grid.Points[r][c].Y) / settings.Parts;
                        var stepX = (grid.Points[r + 1][c].X - grid.Points[r][c].X) / settings.Parts;
                        polyline.AddPoint(new Point(
                            grid.Points[r][c].X + p * stepX, 
                            grid.Points[r][c].Y + p * stepY)
                        );
                    }

                    if (!leftToRight)
                    {
                        polyline.Reverse();
                    }
                    g.AddItem(polyline);
                    leftToRight = !leftToRight;
                }
            }
            else
            {
                
                var polyline = new Polyline(0.3f, "black");
                for (var c = 0; c < grid.Points[r].Length; c++)
                {
                    polyline.AddPoint(grid.Points[r][c]);
                }
                if (!leftToRight)
                {
                    polyline.Reverse();
                }
                g.AddItem(polyline);
            }
        }

        return g; // new Canvas(_ctx.Width, _ctx.Height).WithGroup(g).GetSvg();
    }
}