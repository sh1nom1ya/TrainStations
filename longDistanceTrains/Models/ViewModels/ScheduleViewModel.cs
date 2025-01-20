using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class ScheduleViewModel
{
    public int RouteID { get; set; }
    public string RouteTitle { get; set; }
    public List<Schedules> Schedules { get; set; }
    public DateTime SelectedDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public List<string> Stops { get; set; } 
    public List<string> Cities { get; set; } 
    public double Price { get; set; }
}