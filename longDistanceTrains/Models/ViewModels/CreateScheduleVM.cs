using Microsoft.AspNetCore.Mvc.Rendering;

public class CreateScheduleVM
{
    public int? SelectedRouteId { get; set; }
    public List<SelectListItem> Routes { get; set; }

    public int? SelectedTrainId { get; set; }
    public List<SelectListItem> Trains { get; set; }

    public List<StopVM> Stops { get; set; } = new List<StopVM> { new StopVM() }; // Инициализация с одной остановкой
}

public class StopVM
{
    public string nameStop { get; set; }
    public string timeStop { get; set; }
}