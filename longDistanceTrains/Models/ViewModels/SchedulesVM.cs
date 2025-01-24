namespace longDistanceTrains.Models.ViewModels;

public class SchedulesVM
{
    public int ScheduleID { get; set; }
    public string RouteTitle { get; set; } 
    public List<Stop> Stops { get; set; } 
    public List<TrainViewModel> Trains { get; set; } 
}

public class TrainViewModel
{
    public int TrainNumber { get; set; }
}