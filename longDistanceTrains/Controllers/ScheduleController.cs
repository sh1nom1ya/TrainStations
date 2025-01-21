using longDistanceTrains.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using trains.Data;
using trains.Models;

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
        DateTime selectedDate = selectedDateJson != null
            ? JsonConvert.DeserializeObject<DateTime>(selectedDateJson)
            : DateTime.Now;

        var schedules = _db.schedules
            .Where(s => s.routFK == route.routeID && s.timeDeparture.Date == selectedDate.Date)
            .OrderBy(s => s.timeDeparture)
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


    [HttpPost]
    public IActionResult ForTicketDetails(int scheduleId)
    {
        HttpContext.Session.SetInt32("SelectedScheduleId", scheduleId);

        return RedirectToAction("TicketDetails");
    }

    public IActionResult TicketDetails()
    {
        var scheduleId = HttpContext.Session.GetInt32("SelectedScheduleId");

        var schedule = _db.schedules
            .Include(s => s.Routes)
            .FirstOrDefault(s => s.scheduleID == scheduleId);

        var wagons = _db.wagons.ToList();

        var wagonPrices = wagons.Select(wagon => new
        {
            Name = wagon.title,
            Price = CalculateWagonPrice(schedule.Routes.price, wagon.markupRate)
        }).ToList();

        ViewBag.WagonPrices = wagonPrices;

        return View();
    }
    
    [HttpPost]
    public IActionResult SaveSelection(int Adults, int Children, string WagonType, decimal Price)
    {
        if (Adults < 1 || Adults > 10 || Children < 0 || Children > 10 || string.IsNullOrEmpty(WagonType) || Price <= 0)
        {
            return BadRequest("Некорректные данные.");
        }
        
        decimal adultPrice = Price * Adults;
        decimal childPrice = Price * Children * 0.85m; 
        decimal totalPrice = adultPrice + childPrice;
        
        var ticketDetails = new
        {
            Adults,
            Children,
            WagonType,
            Price,
            AdultPrice = adultPrice,
            ChildPrice = childPrice,
            TotalPrice = totalPrice
        };

        HttpContext.Session.SetString("TicketDetails", JsonConvert.SerializeObject(ticketDetails));
        
        return RedirectToAction("Index", "Payment");
    }

    private double CalculateWagonPrice(double basePrice, string markupRate)
    {
        if (double.TryParse(markupRate.TrimEnd('%'), out double markup))
        {
            return basePrice * (1 + markup / 100);
        }

        return basePrice;
    }
}