using FluentAssertions;
using yesenin.Qaraqulie.Library.Abstractions;

namespace yesenin.Qaraqulie.Library.Test;

public class PathTests
{
    [Fact]
    public void ShouldCorrectlyCalculateMove()
    {
        var p1 = new Point(1, 1);
        var p2 = new Point(10, 10);

        var p = p2.MoveTo(p1, Math.Sqrt(2));
        p.X.Should().Be(p.Y);
        p.X.Should().Be(9);
        
        var q = p1.MoveTo(p2, Math.Sqrt(2));
        q.X.Should().Be(q.Y);
        q.X.Should().Be(2);
    }
}