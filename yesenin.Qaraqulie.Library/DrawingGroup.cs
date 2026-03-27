using System.Text;

namespace yesenin.Qaraqulie.Library;

/// <summary>
/// A wrapper for SVG group
/// </summary>
/// <param name="StrokeColor"></param>
/// <param name="StrokeWidth"></param>
public class DrawingGroup(string StrokeColor, float StrokeWidth) : IDrawable
{
    private List<IDrawableItem> _items = new();
    
    public void AddItem(IDrawableItem item)
    {
        _items.Add(item);
    }
    
    public string GetSvg()
    {
        var groupString = new StringBuilder();
        groupString.AppendLine($"<g fill=\"none\" stroke=\"{StrokeColor}\" stroke-width=\"{StrokeWidth}\">");
        foreach (var drawableItem in _items)
        {
            groupString.AppendLine(drawableItem.GetSvg());
        }
        groupString.AppendLine("</g>");
        return groupString.ToString();
    }
}