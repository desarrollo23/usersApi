using AutoMapper;
using User.Model.DTOs;
using User.Model.Models;

namespace User.Model.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntityDTO, UserEntity>();
        }
    }
}
