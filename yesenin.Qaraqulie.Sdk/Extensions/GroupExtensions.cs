using yesenin.Qaraqulie.Library;

namespace yesenin.Qaraqulie.Sdk.Extensions;

public static class GroupExtensions
{
    public static DrawingGroup WithItem(this DrawingGroup group, IDrawableItem item)
    {
        group.AddItem(item);
        return group;
    }
}