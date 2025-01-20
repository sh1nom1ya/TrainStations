using trains.Models;

namespace longDistanceTrains.Models.ViewModels;

public class MyTicketsVM
{
    public List<Tickets> MyTickets { get; set; }
    public List<TicketDetail> TicketDetails { get; set; }
}

public class TicketDetail
{
    public Tickets Ticket { get; set; }
    public Routes Route { get; set; }
    public List<Schedules> Schedules { get; set; }
}