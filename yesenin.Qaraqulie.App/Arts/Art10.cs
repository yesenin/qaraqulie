using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

public class Art10 : IArt
{
    private readonly CanvasSettings _ctx;
    
    public Art10()
    {
        _ctx = CanvasSettings.DefaultPortraitA4Context();
    }
    
    public string GetSvg()
    {
        var g = new DrawingGroup("blue", 0.1f);
        
        g.AddItem(new Line(_ctx.TopLeft, _ctx.TopRight));
        g.AddItem(new Line(_ctx.TopRight, _ctx.BottomRight));
        g.AddItem(new Line(_ctx.BottomRight, _ctx.BottomLeft));
        g.AddItem(new Line(_ctx.BottomLeft, _ctx.TopLeft));
        
        var halfWidth = _ctx.DrawingAreaWidth / 2;
        
        var squareTopLeft = _ctx.Center + new Point(-halfWidth, -halfWidth);
        var squareTopRight = _ctx.Center + new Point(halfWidth, -halfWidth);
        var squareBottomLeft = _ctx.Center + new Point(-halfWidth, halfWidth);
        var squareBottomRight = _ctx.Center + new Point(halfWidth, halfWidth);
        
        g.AddItem(new Line(squareTopLeft, squareTopRight));
        g.AddItem(new Line(squareTopRight, squareBottomRight));
        g.AddItem(new Line(squareBottomRight, squareBottomLeft));
        g.AddItem(new Line(squareBottomLeft, squareTopLeft));

        var circlesSide = 3;
        
        var widgetSide = _ctx.DrawingAreaWidth / circlesSide;
        var widgetHalfSide = widgetSide / 2;

        for (var y = 0; y < circlesSide; y++)
        {
            for (var x = 0; x < circlesSide; x++)
            {
                g.AddItem(new Dot(squareTopLeft.X + widgetSide * x + widgetHalfSide, squareTopLeft.Y + widgetSide * y + widgetHalfSide));
                for (var r = 0; r < 10; r ++)
                {
                    g.AddItem(new CirclePath(squareTopLeft.X + widgetSide * x + widgetHalfSide, squareTopLeft.Y + widgetSide * y + widgetHalfSide, 5 + r * 1));
                }
            }
        }
        
        
        var svg = new Canvas(_ctx)
            .WithGroup(g)
            .GetSvg();
        return svg;
    }
}