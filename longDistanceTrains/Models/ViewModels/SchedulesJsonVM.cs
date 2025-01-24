namespace longDistanceTrains.Models.ViewModels;

public class Stop
{
    public string nameStop{ get; set; }
    public string timeStop { get; set; }
}

public class SchedulesJsonVM
{
    public List<Stop> stops { get; set; }
}