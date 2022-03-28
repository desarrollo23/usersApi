using AutoMapper;
using User.Model.DTOs;
using User.Model.Models;

namespace User.Model.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntityDTO, UserEntity>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(x => x.Avatar));
        }
    }
}
