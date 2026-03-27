using yesenin.Qaraqulie.Library;

namespace yesenin.Qaraqulie.Sdk.Grid.Renderers;

public interface IGridRenderer
{
    DrawingGroup Render(Grid grid, GridSettings settings);
}