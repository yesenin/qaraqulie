using FluentAssertions;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library.Test;

public class DrawingGroupTest
{
    [Fact]
    public void EmptyGroupShouldHaveCorrectSvg()
    {
        var g = new DrawingGroup("black", 0.1f);
        var svg = g.GetSvg();
        svg.Should().Contain("<g");
        svg.Should().Contain("</g>");
        svg.Split("\n", StringSplitOptions.RemoveEmptyEntries).Length.Should().Be(2);
    }
    
    [Fact]
    public void GroupWithSingleLineShouldHaveCorrectSvg()
    {
        var g = new DrawingGroup("black", 0.1f);
        g.AddItem(new Line(new Point(0, 0), new Point(1, 1)));
        var svg = g.GetSvg();
        svg.Should().Contain("<g");
        svg.Should().MatchRegex("<polyline.+/>");
        svg.Should().Contain("</g>");
        svg.Split("\n", StringSplitOptions.RemoveEmptyEntries).Length.Should().Be(3);
    }
}