using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class TicketUserVM
{
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public Tickets Ticket { get; set; }
}