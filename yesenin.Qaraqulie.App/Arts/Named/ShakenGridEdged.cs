using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Sdk.Extensions;
using yesenin.Qaraqulie.Sdk.Grid;
using yesenin.Qaraqulie.Sdk.Grid.Renderers;

namespace yesenin.Qaraqulie.App.Arts.Named;

public class ShakenGridEdged : IArt
{
    private readonly Grid _grid;
    private readonly CanvasSettings _ctx;
    private readonly GridSettings _gridSettings;
    
    public ShakenGridEdged()
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
        
        _gridSettings = new GridSettings
        {
            GridWidth = 22,
            GridHeight = 15,
            Parts = 12,
            ShakeIntensity = 7.0,
            FixedBorder = false
        };
        
        _grid = new Grid(_ctx, _gridSettings);
        _grid.Shake(_gridSettings.ShakeIntensity);
    }
    
    public string GetSvg()
    {
        var edgeRendered = new GridCurveRenderer();
        var svg = new Canvas(_ctx).WithGroup( edgeRendered.Render(_grid, _gridSettings)).GetSvg();
        return svg;
    }
}