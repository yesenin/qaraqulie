using Microsoft.AspNetCore.Mvc;
using yesenin.Qaraqulie.Web.Models;

namespace yesenin.Qaraqulie.Web.Controllers;

public class ArtController : Controller
{
    // GET
    public IActionResult Index()
    {
        var viewModel = new List<ArtFolder>();
        var artFolder = "/home/yesenin/Development/output/qaraqulie";
        foreach (var directory in Directory.GetDirectories(artFolder))
        {
            var folderName = Path.GetFileName(directory);
            var foo = new ArtFolder()
            {
                Name = folderName,
            };
            foreach (var file in Directory.GetFiles(directory))
            {
                
                foo.Files.Add($"{folderName}/{Path.GetFileName(file)}");
            }
            viewModel.Add(foo);
        }
        return View(viewModel);
    }
}