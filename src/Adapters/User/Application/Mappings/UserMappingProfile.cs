using Adapters.User.Application.DTOs;
using Adapters.User.Domain.DTOs;
using Adapters.User.Infra.Entities;
using AutoMapper;
using Users = Adapters.User.Domain.DTOs.User;

namespace Adapters.User.Application.Mappings
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            // Domain <=> DTO
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
            // Domain <=> Infra Entity
            CreateMap<Users, UserEntity>().ReverseMap();
            CreateMap<UserProfile, UserProfileEntity>().ReverseMap();
        }
    }
}
