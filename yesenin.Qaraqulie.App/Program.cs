// See https://aka.ms/new-console-template for more information

using yesenin.Qaraqulie.App;
using yesenin.Qaraqulie.App.Arts;
using yesenin.Qaraqulie.App.Arts.Named;

using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var outputPath = Environment.GetEnvironmentVariable("OUTPUT_PATH") ?? "";

try
{
    var gridName = $"art_{DateTime.Now:yyyyMMddHHmmss}";
    var bundle = new TaskBundle()
        .AddTask(new TaskItem(new ShakenGridEdged(gridName), Path.Combine(outputPath, $"{gridName}.svg")));

    bundle.Execute();
    
    Console.WriteLine("Success!");
}
catch (Exception e)
{
    Console.WriteLine($"ERR: {e.Message}");
}