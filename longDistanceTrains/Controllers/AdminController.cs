using longDistanceTrains.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        var tickets = await _db.tickets
            .Include(t => t.User)
            .ToListAsync();

        var users = await _db.users
            .ToListAsync();

        
        var ticketUserVMs = new List<TicketUserVM>();

        foreach (var ticket in tickets)
        {
             if (ticket.userID != null)
             {
                 var user = ticket.User;
                 if (user != null)
                 {
                     var appUser = user;
                     if (appUser != null)
                     {
                         ticketUserVMs.Add(new TicketUserVM
                         {
                             Fullname = appUser.FullName,
                             Phone = appUser.PhoneNumber,
                             Ticket = ticket
                         });
                     }
                 }
             }
        }


        return View(ticketUserVMs);
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

    public IActionResult TrainRedaction(int? trainID)
    {
        TrainVM trainVM = new TrainVM()
        {
            WagonsList = _db.wagons.Select(i => new SelectListItem
            {
                Text = i.title,
                Value = i.wagonID.ToString()
            }),
        };

        if (!trainID.HasValue)
        {
            return View(trainVM);
        }
        else
        {
            trainVM.Train = _db.trains.Find(trainID.Value);

            if (trainVM.Train == null)
            {
                return NotFound();
            }

            return View(trainVM);
        }
    }

    public IActionResult SaveTrain(TrainVM trainVM)
    {
        if (!ModelState.IsValid)
        {
            if (trainVM.Train.trainID == 0)
            {
                _db.trains.Add(trainVM.Train);
            }
            else
            {
                _db.trains.Update(trainVM.Train);
            }

            _db.SaveChanges();
            return RedirectToAction("TrainsBrowse");
        }

        trainVM.WagonsList = _db.wagons.Select(i => new SelectListItem
        {
            Text = i.title,
            Value = i.wagonID.ToString()
        });
        return View(trainVM);
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

    public IActionResult RoutesRedaction(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new Routes());
        }
        else
        {
            var route = _db.routes.Find(id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }
    }

    [HttpPost]
    public IActionResult SaveRoute(string departureCity, string arrivalCity, string stops, double price)
    {
        if (ModelState.IsValid)
        {
            string routeTitle = $"{departureCity}/{arrivalCity}";

            var route = new Routes
            {
                title = routeTitle,
                stops = stops,
                price = price
            };

            _db.routes.Add(route);
            _db.SaveChanges();

            return RedirectToAction("RoutesBrowse");
        }

        return View("RoutesRedaction");
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


    public async Task<IActionResult> SchedulesBrowse()
    {
        var scheduleDbModel = await _db.schedules
            .Include(s => s.Routes)
            .FirstOrDefaultAsync();

        if (scheduleDbModel == null)
        {
            return NotFound();
        }

        var scheduleData = JsonConvert.DeserializeObject<SchedulesJsonVM>(scheduleDbModel.timeStops);

        Console.WriteLine(scheduleData.stops);

        var trains = await _db.trains
            .Where(t => t.scheduleFK == scheduleDbModel.scheduleID)
            .ToListAsync();

        var schedulesVM = new SchedulesVM()
        {
            RouteTitle = scheduleDbModel.Routes.title,
            Stops = scheduleData.stops,
            Trains = trains.Select(t => new TrainViewModel
            {
                TrainNumber = t.num,
            }).ToList()
        };

        return View(schedulesVM);
    }

    public IActionResult SchedulesRedaction()
    {
        return View();
    }
}