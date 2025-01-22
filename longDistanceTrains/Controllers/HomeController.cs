using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using longDistanceTrains.Models;
using longDistanceTrains.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        if (User.IsInRole("admin"))
        {
            return RedirectToAction("Index", "Admin");
        }
        
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
            .Where(s => s.routFK == route.routeID && s.timeDeparture.Date == date.Date)
            .ToList();

        var routeVM = new RouteVM()
        {
            RouteTitle = route.title.Trim()
        };

        var routeVMJson = JsonConvert.SerializeObject(routeVM);
        HttpContext.Session.SetString("RouteVM", routeVMJson);

        var selectedDateJson = JsonConvert.SerializeObject(date);
        HttpContext.Session.SetString("SelectedDate", selectedDateJson);

        return RedirectToAction("Index", "Schedule");
    }
    
    [Authorize]
    public IActionResult MyTickets()
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var userTickets = _db.tickets
            .Where(t => t.userID == userID)
            .ToList();
        
        var ticketDetails = new List<TicketDetail>();

        foreach (var ticket in userTickets)
        {
            var route = _db.routes.FirstOrDefault(r => r.routeID == ticket.routFK);

            if (route != null)
            {
                var schedules = _db.schedules
                    .Where(s => s.routFK == route.routeID)
                    .ToList();
                
                ticketDetails.Add(new TicketDetail
                {
                    Ticket = ticket,
                    Route = route,
                    Schedules = schedules
                });
            }
        }
        
        var myTicketsVM = new MyTicketsVM
        {
            MyTickets = userTickets,
            TicketDetails = ticketDetails
        };

        return View(myTicketsVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}