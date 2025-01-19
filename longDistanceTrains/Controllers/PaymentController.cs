using Microsoft.AspNetCore.Mvc;
using trains.Data;

namespace longDistanceTrains.Controllers;

public class PaymentController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TrainDbContext _db;

    public PaymentController(ILogger<HomeController> logger, TrainDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Check()
    {
        return View();
    }
}