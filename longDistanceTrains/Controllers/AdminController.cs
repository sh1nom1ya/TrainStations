using Microsoft.AspNetCore.Mvc;
using trains.Data;

namespace longDistanceTrains.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TrainDbContext _db;

    public AdminController(ILogger<HomeController> logger, TrainDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult TicketsBrowse()
    {
        return View();
    }
    
    public IActionResult TicketsRedaction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTicket()
    {
        return View();
    }
    
    
    public IActionResult TrainsBrowse()
    {
        return View();
    }
    
    public IActionResult TrainsRedaction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTrain()
    {
        return View();
    }
    
    
    public IActionResult RoutesBrowse()
    {
        return View();
    }
    
    public IActionResult RoutesRedaction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteRoute()
    {
        return View();
    }
    
    
    public IActionResult SchedulesBrowse()
    {
        return View();
    }
    
    public IActionResult SchedulesRedaction()
    {
        return View();
    }
}