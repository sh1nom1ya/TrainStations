using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Layouts
{
    [Required]
    public int trainID { get; set; }
    [ForeignKey("trainID")]
    public virtual Trains Train { get; set; }
    
    [Required]
    public int wagonID { get; set; }
    [ForeignKey("wagonID")]
    public virtual Wagons Wagon { get; set; }
    
    [Required]
    public int serialNum { get; set; }
}