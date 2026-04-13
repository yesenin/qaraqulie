using System.Drawing;
using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

public class Art09 : IArt
{
    private readonly CanvasSettings _ctx;
    
    public Art09()
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
    }
    
    public string GetSvg()
    {
        var g = new DrawingGroup("red", 0.1f);
        
        g.AddItem(new Line(_ctx.TopLeft, _ctx.TopRight));
        g.AddItem(new Line(_ctx.TopRight, _ctx.BottomRight));
        g.AddItem(new Line(_ctx.BottomRight, _ctx.BottomLeft));
        g.AddItem(new Line(_ctx.BottomLeft, _ctx.TopLeft));
        
        var svg = new Canvas(_ctx)
            .WithGroup(g)
            .GetSvg();
        return svg;
    }
}