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
    
    public IActionResult AllTickets()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTickets()
    {
        return View();
    }
    
    //нету
    public IActionResult AllTrains()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTrain()
    {
        return View();
    }
    
    public IActionResult CreateTrain()
    {
        return View();
    }
    
    public IActionResult AllRoutes()
    {
        return View();
    }
    
    public IActionResult CreateRoute()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteRoute()
    {
        return View();
    }
    
    public IActionResult AllSchedules()
    {
        return View();
    }
    
    public IActionResult CreateSchedule()
    {
        return View();
    }
}