using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Sdk.Extensions;
using yesenin.Qaraqulie.Sdk.Grid;
using yesenin.Qaraqulie.Sdk.Grid.Renderers;

namespace yesenin.Qaraqulie.App.Arts.Named;

public class ShakenGridCurved : IArt
{
    private readonly Grid _grid;
    private readonly CanvasSettings _ctx;
    private readonly GridSettings _gridSettings;
    
    public ShakenGridCurved(string name, int width=25, int height=15, int parts=12, double shakeIntensity=7.0)
    {
        _ctx = CanvasSettings.DefaultLandscapeA4Context();
        
        _gridSettings = new GridSettings
        {
            Name = name,
            Width = width,
            Height = height,
            Parts = parts,
            ShakeIntensity = shakeIntensity
        };
        
        _grid = new Grid(_ctx, _gridSettings);
        _grid.Shake(_gridSettings.ShakeIntensity);
    }
    
    public string GetSvg()
    {
        var curveRenderer = new GridCurveRenderer();
        var svg = new Canvas(_ctx).WithGroup( curveRenderer.Render(_grid, _gridSettings)).GetSvg();
        return svg;
    }
}