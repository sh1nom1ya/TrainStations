using longDistanceTrains.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using trains.Data;

namespace longDistanceTrains.Controllers;

public class ScheduleController : Controller
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly TrainDbContext _db;
    
    public ScheduleController(ILogger<ScheduleController> logger, TrainDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public IActionResult Index()
    {
        var routeVMJson = HttpContext.Session.GetString("RouteVM");

        if (string.IsNullOrEmpty(routeVMJson))
        {
            return RedirectToAction("Error", "Home");
        }

        var routeVM = JsonConvert.DeserializeObject<RouteVM>(routeVMJson);

        var route = _db.routes.FirstOrDefault(r => r.title.Trim() == routeVM.RouteTitle.Trim());

        if (route == null)
        {
            ViewBag.Error = "Маршрут не найден";
            return View("Error");
        }
        
        var selectedDateJson = HttpContext.Session.GetString("SelectedDate");
        DateTime selectedDate = selectedDateJson != null ? JsonConvert.DeserializeObject<DateTime>(selectedDateJson) : DateTime.Now;
        
        var schedules = _db.schedules
            .Where(s => s.routFK == route.routeID && s.timeDeparture.Date == selectedDate.Date)
            .ToList();

        DateTime endDate = DateTime.Now;
        if (schedules.Any())
        {
            var maxArrivalTime = schedules.Max(s => s.timeArrival);
            endDate = maxArrivalTime.Date;
        }

        var scheduleViewModel = new ScheduleViewModel
        {
            RouteID = route.routeID,
            RouteTitle = route.title,
            Schedules = schedules,
            SelectedDate = selectedDate,
            EndDate = endDate,
            Stops = route.stops.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList(),
            Cities = route.title.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList(),
            Price = route.price
        };

        return View(scheduleViewModel);
    }
    
    public IActionResult TicketDetails()
    {
        return View();
    }
}