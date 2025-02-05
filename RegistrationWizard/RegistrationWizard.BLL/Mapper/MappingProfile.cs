﻿using AutoMapper;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserRequestDTO>();

            CreateMap<UserRequestDTO, AppUser>();

            CreateMap<Country, CountryResponseDTO>()
            .ForMember(dest => dest.Provinces, opt => opt.MapFrom(src => src.Provinces));

            CreateMap<Province, ProvinceResponceDTO>();

        }
    }
}
