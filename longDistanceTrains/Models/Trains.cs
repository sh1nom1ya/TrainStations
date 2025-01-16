using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Trains
{
    [Key]
    public int trainID { get; set; }
    
    public int num { get; set; }
    
    public int scheduleFK { get; set; }
    [ForeignKey("scheduleFK")]
    public virtual Schedules Schedule { get; set; }
}