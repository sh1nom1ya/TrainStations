using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using longDistanceTrains.Models;
using trains.Data;

namespace longDistanceTrains.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TrainDbContext _db;

    public HomeController(ILogger<HomeController> logger, TrainDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        var routes = _db.routes.Select(r => r.title).ToList();

        if (routes == null || !routes.Any())
        {
            ViewBag.Stations = new List<string>(); // Пустой список, если данных нет
            return View();
        }

        var stations = new HashSet<string>();
        foreach (var route in routes)
        {
            var parts = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                stations.Add(part.Trim());
            }
        }

        ViewBag.Stations = stations.OrderBy(s => s).ToList();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}