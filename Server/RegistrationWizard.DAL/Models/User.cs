namespace RegistrationWizard.DAL.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}
