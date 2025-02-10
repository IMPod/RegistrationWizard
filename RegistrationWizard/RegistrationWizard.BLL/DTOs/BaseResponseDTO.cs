namespace RegistrationWizard.BLL.DTOs;

public class BaseResponseDto<T>
{
    public required List<T> Data { get; init; }
}
