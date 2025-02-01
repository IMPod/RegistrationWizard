namespace RegistrationWizard.DTOs;

public class ErrorDTO
{
    public ErrorDTO()
    {

    }

    public ErrorDTO(string message)
    {
        Error = message;
    }

    public ErrorDTO(Exception ex)
    {
        Error = ex.Message;
        StackTrace = ex.StackTrace;
        InnerError = ex.InnerException?.Message;
        InnerStackTrace = ex.InnerException?.StackTrace;
    }

    public string Error { get; set; }

    public string StackTrace { get; set; }

    public string InnerError { get; set; }

    public string InnerStackTrace { get; set; }
}
