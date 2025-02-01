namespace RegistrationWizard.DAL.Models;
public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
}
