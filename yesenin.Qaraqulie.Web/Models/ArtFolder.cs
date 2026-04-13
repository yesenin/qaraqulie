namespace yesenin.Qaraqulie.Web.Models;

public class ArtFolder
{
    public required string Name { get; init; }

    public List<string> Files = new List<string>();
}