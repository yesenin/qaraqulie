using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Sdk.Grid;

public class HexGrid
{
    public HexGrid(CanvasSettings ctx, HexGridSettings gridSettings)
    {
        var points = new List<Point>();
        var row = 1;

        var cellSize = ctx.DrawingAreaWidth / gridSettings.EvenWidth;
        var sideSize = cellSize / 2;

        for (var r = 0; r < gridSettings.Height; r++)
        {
            var isEventRow = (r + 1) % 2 == 0;
            var hexInRow = isEventRow? gridSettings.EvenWidth :  gridSettings.OddWidth;
            var shift = isEventRow ? 0 : sideSize;
            for (var c = 0; c < hexInRow; c++)
            {
            }
        }
    }
}