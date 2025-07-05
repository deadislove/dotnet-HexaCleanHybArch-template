using Adapters.User.Application.DTOs;
using AutoMapper;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Users;
using HexaCleanHybArch.Template.Core.DTOs.Response.Users;
using HexaCleanHybArch.Template.Shared.Auth;

namespace HexaCleanHybArch.Template.Core.Mapping
{
    public class CoreMappingProfile: Profile
    {
        public CoreMappingProfile() {
            // Core <=> Adapter
            CreateMap<RegisterCoreRequest, UserDto>().ReverseMap();
            
            // Login
            CreateMap<LoginCoreRequest, JwtUserInfo>().ReverseMap();
            CreateMap<UserDto, JwtUserInfo>().ReverseMap();

            // User
            CreateMap<UserCoreRequest, UserDto>().ReverseMap();
            CreateMap<UserCoreResponse, UserDto>().ReverseMap();
            CreateMap<UsersCoreResponse, UsersDto>().ReverseMap();
        }
    }
}
