using Adapters.User.Application.DTOs;
using Adapters.User.Application.Interfaces.Facades;
using AutoMapper;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Users;
using HexaCleanHybArch.Template.Core.DTOs.Response.Users;
using HexaCleanHybArch.Template.Core.Interfaces.Users;

namespace HexaCleanHybArch.Template.Core.Services.Users
{
    public class UserService : IUserServices
    {
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;

        public UserService(IUserFacade userFacade, IMapper mapper) { 
            _userFacade = userFacade;
            _mapper = mapper;
        }
        public async Task CreateUserAsync(UserCoreRequest userDto)
        {
            await _userFacade.CreateUserAsync(userDto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userFacade.DeleteUserAsync(id);
        }

        public async Task<UserCoreResponse> GetUserAsync(UserCoreRequest userDto)
        {
            UserDto result = await _userFacade.GetUserAsync(userDto);
            UserCoreResponse response = _mapper.Map<UserCoreResponse>(result);

            return response;
        }

        public async Task<UsersCoreResponse> GetUsersAsync()
        {
            UsersDto usersDto = await _userFacade.GetUsersAsync();
            UsersCoreResponse response = _mapper.Map<UsersCoreResponse>(usersDto);
            return response;
        }

        public async Task UpdateUserAsync(UserCoreRequest userDto)
        {
            await _userFacade.UpdateUserAsync(userDto);
        }
    }
}
