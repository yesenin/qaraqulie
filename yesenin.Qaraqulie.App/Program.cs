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
    var folderName = "ShakenGridCurved";
    if (!Directory.Exists(Path.Combine(outputPath, folderName)))
    {
        Directory.CreateDirectory(Path.Combine(outputPath, folderName));
    }
    var artFolder = Path.Combine(outputPath, folderName);
    var gridName = $"{DateTime.Now:yyyyMMddHHmmss}";
    var fullPath = $"{Path.Combine(artFolder, gridName)}.svg";
    var bundle = new TaskBundle()
        .AddTask(new TaskItem(new ShakenGridCurved(
            gridName, width: 15, height: 10, parts: 25, shakeIntensity: 10.0), fullPath));

    bundle.Execute();
    
    Console.WriteLine($"Success! {fullPath}");
}
catch (Exception e)
{
    Console.WriteLine($"ERR: {e.Message}");
}