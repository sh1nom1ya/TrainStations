using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Routes
{
    [Key]
    public int routeID { get; set; }
    
    [Required]
    public string title { get; set; }
    
    [Required]
    [Column(TypeName = "text")]
    public string stops { get; set; }
    
    [Required]
    public double price { get; set; }
}