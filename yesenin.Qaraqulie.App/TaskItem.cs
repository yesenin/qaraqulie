using yesenin.Qaraqulie.App.Arts;

namespace yesenin.Qaraqulie.App;

public class TaskItem(IArt art, string fileName)
{
    public string FileName => fileName;
    
    public void Execute() => File.WriteAllText(fileName, art.GetSvg());
}