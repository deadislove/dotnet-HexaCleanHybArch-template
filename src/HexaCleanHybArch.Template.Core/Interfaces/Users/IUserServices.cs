using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Users;
using HexaCleanHybArch.Template.Core.DTOs.Response.Users;

namespace HexaCleanHybArch.Template.Core.Interfaces.Users
{
    public interface IUserServices
    {
        Task<UserCoreResponse> GetUserAsync(UserCoreRequest userDto);
        Task<UsersCoreResponse> GetUsersAsync();
        Task UpdateUserAsync(UserCoreRequest userDto);
        Task DeleteUserAsync(Guid id);
        Task CreateUserAsync(UserCoreRequest userDto);
    }
}
