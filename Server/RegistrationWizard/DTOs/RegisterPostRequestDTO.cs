namespace RegistrationWizard.DTOs;

public class RegisterPostRequestDTO
{
    public string Message { get; internal set; }
    public string Errors { get; internal set; }
    public bool IsError { get; internal set; } = false;
    public bool Success { get; internal set; } = true;
}
