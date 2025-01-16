using Microsoft.AspNetCore.Identity;

namespace trains.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}