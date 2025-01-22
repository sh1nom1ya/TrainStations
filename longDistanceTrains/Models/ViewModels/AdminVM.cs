using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class AdminVM
{
    public Tickets Ticket { get; set; }
    public Routes Route { get; set; }
    public Trains Train { get; set; }
    public Schedules Schedule { get; set; }
}