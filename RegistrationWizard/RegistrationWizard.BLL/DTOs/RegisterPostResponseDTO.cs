namespace RegistrationWizard.BLL.DTOs;

public class RegisterPostResponseDTO
{
    public string Message { get; set; }
    public string Errors { get; set; }
    public bool IsError { get; set; } = false;
    public bool Success { get; set; } = true;
}
