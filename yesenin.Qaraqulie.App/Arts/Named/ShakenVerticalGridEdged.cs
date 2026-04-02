using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Sdk.Extensions;
using yesenin.Qaraqulie.Sdk.Grid;
using yesenin.Qaraqulie.Sdk.Grid.Renderers;

namespace yesenin.Qaraqulie.App.Arts.Named;

public class ShakenVerticalGridEdged : IArt
{
    private readonly Grid _grid;
    private readonly CanvasSettings _ctx;
    private readonly GridSettings _gridSettings;
    
    public ShakenVerticalGridEdged(string name)
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
        _ctx.TopMargin += 20;
        _ctx.BottomMargin += 20;
        _ctx.LeftMargin += 20;
        _ctx.RightMargin += 20;
        
        _gridSettings = new GridSettings
        {
            Name = name,
            Width = 22,
            Height = 12,
            Parts = 15,
            ShakeIntensity = 7.0
        };
        
        _grid = new Grid(_ctx, _gridSettings);
        _grid.Shake(_gridSettings.ShakeIntensity);
    }
    
    public string GetSvg()
    {
        var horizontalRenderer = new GridEdgeRenderer();
        var edgeRendered = new GridVerticalEdgeRenderer();
        var svg = new Canvas(_ctx)
            .WithGroup( edgeRendered.Render(_grid, _gridSettings))
            .WithGroup( horizontalRenderer.Render(_grid, _gridSettings))
            .GetSvg();
        return svg;
    }
}