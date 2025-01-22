using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Layouts
{
    [Key]
    public int layoutID { get; set; }
    
    [Required]
    public int trainID { get; set; }
    [ForeignKey("trainID")]
    public virtual Trains Train { get; set; }
    
    public int wagonID { get; set; }
    [ForeignKey("wagonID")]
    public virtual Wagons Wagon { get; set; }
    
    [Required]
    public int serialNum { get; set; }
}