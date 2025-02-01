namespace RegistrationWizard.DTOs;

public class CountryResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ProvinceResponceDTO> Provinces { get; set; } = new List<ProvinceResponceDTO>();
}
