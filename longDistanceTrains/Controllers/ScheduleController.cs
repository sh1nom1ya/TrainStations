using Microsoft.AspNetCore.Mvc;
using trains.Data;

namespace longDistanceTrains.Controllers;

public class ScheduleController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TrainDbContext _db;
    
    public ScheduleController(ILogger<HomeController> logger, TrainDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}