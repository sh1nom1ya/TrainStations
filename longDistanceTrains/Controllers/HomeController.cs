using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using longDistanceTrains.Models;
using longDistanceTrains.Models.ViewModels;
using Newtonsoft.Json;
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
             ViewBag.Stations = new List<string>();
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
    
    [HttpPost]
    public IActionResult FindSchedule(string cityFrom, string cityTo, DateTime date)
    {
        var routeTitle = $"{cityFrom}/{cityTo}";

        var route = _db.routes.FirstOrDefault(r => r.title.Trim() == routeTitle.Trim());

        if (route == null)
        {
            ViewBag.Error = "Маршрут не найден";
            return View("Index");
        }

        var schedules = _db.schedules
            .Where(s => s.routFK == route.routeID)
            .ToList();

        var routeVM = new RouteVM()
        {
            RouteTitle = route.title.Trim()
        };

        var routeVMJson = JsonConvert.SerializeObject(routeVM);
        HttpContext.Session.SetString("RouteVM", routeVMJson);

        return RedirectToAction("Index", "Schedule");
    }
    
    public IActionResult MyTickets()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}