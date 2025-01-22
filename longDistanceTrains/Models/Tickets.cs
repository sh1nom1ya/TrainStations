using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Tickets
{
    [Key]
    public int ticketID { get; set; }

    [Required]
    public double price { get; set; }
    
    public string QR { get; set; } = String.Empty;
    
    [Required]
    public DateTime timeProcessing { get; set; }

    [Required]
    public string wagonType { get; set; }
    
    public int adults { get; set; }
    
    public int children { get; set; }

    [Required]
    public int routeFK { get; set; }
    [ForeignKey("routeFK")]
    public virtual Routes Route { get; set; }
    
    [Required]
    public string userID { get; set; }
    [ForeignKey("userID")]
    public virtual AppUser User { get; set; }
}