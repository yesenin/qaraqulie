using yesenin.Qaraqulie.Library;
using yesenin.Qaraqulie.Library.Abstractions;
using yesenin.Qaraqulie.Sdk;
using yesenin.Qaraqulie.Sdk.Extensions;

namespace yesenin.Qaraqulie.App.Arts;

/// <summary>
/// Just a cross
/// </summary>
public class Art01 : IArt
{
    private readonly Canvas _canvas = new Canvas(100, 100)
        .WithGroup(
            new DrawingGroup("black", 0.1f)
                .WithItem(new Line(new Point(0, 0), new Point(10, 10)))
                .WithItem(new Line(new Point(10, 1), new Point(1, 10)))
        );

    public string GetSvg() => _canvas.GetSvg();
}