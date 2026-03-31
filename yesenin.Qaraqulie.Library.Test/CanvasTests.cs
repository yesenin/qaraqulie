using FluentAssertions;

namespace yesenin.Qaraqulie.Library.Test;

public class CanvasTests
{
    [Fact]
    public void CratedCanvasShouldHaveCorrectSize()
    {
        var canvas = new Canvas(100, 200);
        
        var svg = canvas.GetSvg();
        
        canvas.Width.Should().Be(100);
        canvas.Height.Should().Be(200);

        svg.Should().Contain("width=\"100mm\"");
        svg.Should().Contain("height=\"200mm\"");
    }

    [Fact]
    public void EmptyCanvasShouldHaveCorrectSvg()
    {
        var canvas = new Canvas(100, 200);
        var svg = canvas.GetSvg();
        svg.Should().NotBeNullOrEmpty();
        svg.Should().StartWith("<svg");
        // TODO: check with regex
        svg.Should().Contain("\"http://www.w3.org/2000/svg\"");
        svg.Should().Contain("width");
        svg.Should().Contain("height");
        svg.Should().Contain("viewBox");
        svg.Should().NotContain("<g");
        svg.Should().EndWith("</svg>\n");
    }

    [Fact]
    public void CanvasWithEmptyGroupShouldHaveCorrectSvg()
    {
        var canvas = new Canvas(100, 200);
        var g = new DrawingGroup("black", 0.1f);
        canvas.AddGroup(g);
        var svg = canvas.GetSvg();
        svg.Should().Contain("<g");
        svg.Should().Contain("</g>");
    }
}
