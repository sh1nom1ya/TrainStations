using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Schedules
{
    [Key]
    public int scheduleID { get; set; }
    
    [Required]
    public DateTime timeArrival { get; set; }
    
    [Required]
    public DateTime timeDeparture { get; set; }
    
    [Required]
    [Column(TypeName = "jsonb")]
    public string timeStops { get; set; }
    
    [Required]
    public int routFK { get; set; }
    [ForeignKey("routFK")]
    public virtual Routes Routes { get; set; }
}