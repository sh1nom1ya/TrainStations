using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class TicketUserVM
{
    public Tickets Ticket { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
}