using longDistanceTrains.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using trains.Data;
using trains.Models;

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
    
    public async Task<IActionResult> TicketsBrowse()
    {
        return View();
    }
    
    public IActionResult TicketsRedaction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTicket(int? ticketId)
    {
        // var ticket = _db.tickets.Find(ticketId);
        //
        // if (ticket == null)
        // {
        //     return NotFound();
        // }
        //
        // _db.routes.Remove(ticket);
        // _db.SaveChanges();
        
        return RedirectToAction("TicketsBrowse");
    }
    
    
    public IActionResult TrainsBrowse()
    {
        var trains = _db.trains
            .Include(t => t.Layouts)
            .ThenInclude(l => l.Wagon)
            .ToList();

        return View(trains);
    }
    
    public IActionResult TrainsRedaction()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteTrain(int? trainID)
    {
        var train = _db.trains
            .Include(t => t.Layouts)
            .FirstOrDefault(t => t.trainID == trainID);

        if (train == null)
        {
            return NotFound();
        }
        
        _db.layouts.RemoveRange(train.Layouts);
        _db.trains.Remove(train);
        _db.SaveChanges();

        return RedirectToAction("TrainsBrowse"); 
    }
    
    
    public IActionResult RoutesBrowse()
    {
        IEnumerable<Routes> routes = _db.routes;
        return View(routes);
    }
    
    public IActionResult RoutesRedaction()
    {
        
        return View();
    }
    
    [HttpPost]
    public IActionResult DeleteRoute(int? routeId)
    {
        var route = _db.routes.Find(routeId);

        if (route == null)
        {
            return NotFound();
        }
        
        _db.routes.Remove(route);
        _db.SaveChanges();
        
        return RedirectToAction("RoutesBrowse");
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