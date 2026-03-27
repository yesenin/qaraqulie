// See https://aka.ms/new-console-template for more information

using yesenin.Qaraqulie.App;
using yesenin.Qaraqulie.App.Arts;
using yesenin.Qaraqulie.App.Arts.Named;

try
{
    var bundle = new TaskBundle()
        .AddTask(new TaskItem(new Art05(), "art05.svg"));

    bundle.Execute();
    
    Console.WriteLine("Success!");
}
catch (Exception e)
{
    Console.WriteLine($"ERR: {e.Message}");
}