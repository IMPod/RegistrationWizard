namespace RegistrationWizard.DTOs;

public class BaseResponseDTO<T>
{
    public List<T> Data { get; set; }
}
