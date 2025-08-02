using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Model;
using ZAMETKI_FINAL.Services;
using ZAMETKI_FINAL.Dto_Vm;

namespace ZAMETKI_FINAL.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
