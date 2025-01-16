using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Tickets
{
    [Key]
    public int ticketID { get; set; }

    [Required]
    public double price { get; set; }
    
    public string QR { get; set; }
    
    [Required]
    public DateTime timeProcessing { get; set; }
    
    [Required]
    public int routFK { get; set; }
    [ForeignKey("routeFK")]
    public virtual Routes Route { get; set; }
    
    [Required]
    public string userID { get; set; }
    [ForeignKey("userID")]
    public virtual AppUser User { get; set; }
}