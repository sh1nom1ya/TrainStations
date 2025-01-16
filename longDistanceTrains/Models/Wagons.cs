using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trains.Models;

public class Wagons
{
    [Key]
    public int wagonID { get; set; }
    
    [Required]
    public string title { get; set; }
    
    [Required]
    public string markupRate { get; set; }
}