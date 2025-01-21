using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        var ticketDetailsJson = HttpContext.Session.GetString("TicketDetails");

        if (string.IsNullOrEmpty(ticketDetailsJson))
        {
            return RedirectToAction("Error", "Home");
        }

        var ticketDetails = JsonConvert.DeserializeObject<dynamic>(ticketDetailsJson);

        ViewBag.TicketDetails = ticketDetails;

        return View();
    }
}