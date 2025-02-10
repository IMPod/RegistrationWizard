using AutoMapper;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserRequestDto>();

        CreateMap<UserRequestDto, AppUser>();

        CreateMap<Country, CountryResponseDTO>()
            .ForMember(dest => dest.Provinces, opt => opt.MapFrom(src => src.Provinces));

        CreateMap<Province, ProvinceResponceDTO>();
        CreateMap<CreateUserCommand, AppUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(u => u.ProvinceId, opt => opt.MapFrom(src => src.ProvinceId));
        
        CreateMap<AppUser, UserResponseDto>();
    }
}