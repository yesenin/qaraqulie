using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk.Extensions;
using yesenin.Qaraqulie.Sdk.Grid;
using yesenin.Qaraqulie.Sdk.Grid.Renderers;

namespace yesenin.Qaraqulie.App.Arts;

public class Art06 : IArt
{
    private Grid _grid;
    private readonly CanvasSettings _ctx;
    private readonly GridSettings _gridSettings;
    
    public Art06()
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
    }
    
    public string GetSvg()
    {
        var g = new DrawingGroup("red", 0.1f);

        var point1 = new Point(RandomX(), RandomY());
        var point2 = new Point(RandomX(), RandomY());
        var point3 = new Point(RandomX(), RandomY());

        g.AddItem(new Line(_ctx.TopLeft, _ctx.TopRight));
        g.AddItem(new Line(_ctx.BottomLeft, _ctx.BottomRight));
        g.AddItem(new Line(_ctx.TopLeft, _ctx.BottomLeft));
        g.AddItem(new Line(_ctx.TopRight, _ctx.BottomRight));
        
        
        g.AddItem(new Line(point1, point2));
        g.AddItem(new Line(point2, point3));
        g.AddItem(new Line(point3, point1));
        
        var horizontalRenderer = new GridCurveRenderer();
        var svg = new Canvas(_ctx)
            .WithGroup(g)
            .GetSvg();
        return svg;
    }

    private double RandomX()
    {
        var drawingWidth = _ctx.Width - _ctx.RightMargin;
        return Random.Shared.NextDouble() * drawingWidth + _ctx.LeftMargin;
    }
    
    private double RandomY()
    {
        var drawingHeight = _ctx.Height - _ctx.BottomMargin;
        return Random.Shared.NextDouble() * drawingHeight + _ctx.TopMargin;
    }
}