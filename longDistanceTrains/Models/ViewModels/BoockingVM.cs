using System.ComponentModel.DataAnnotations;

public class BookingViewModel
{
    public int Adults { get; set; }
    
    public int Children { get; set; }
    
    public string WagonType { get; set; }
    
    public double Price { get; set; }

    public double AdultPrice { get; set; }
    public double ChildPrice { get; set; }
    public double TotalPrice { get; set; }
}