using Microsoft.AspNetCore.Identity;

namespace RegistrationWizard.DAL.Models;

public class AppUser : IdentityUser<int>
{
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}
