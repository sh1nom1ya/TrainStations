using Microsoft.AspNetCore.Mvc.Rendering;
using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class TrainVM
{
    public Trains Train { get; set; }
    public IEnumerable<SelectListItem> WagonsList { get; set; }
}