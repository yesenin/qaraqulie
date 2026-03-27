using yesenin.Qaraqulie.Library;

namespace yesenin.Qaraqulie.Sdk.Extensions;

public static class CanvasExtensions
{
    public static Canvas WithGroup(this Canvas canvas, DrawingGroup grop)
    {
        canvas.AddGroup(grop);
        return canvas;
    }
}