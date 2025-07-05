using Adapters.User.Application.DTOs;

namespace Adapters.User.Application.Interfaces.Facades
{
    public interface IUserFacade
    {
        Task<UserDto?> ValidateUserAsync(string email, string password = "");
        Task RegisterUserAsync(UserDto userDto);
        Task<bool> IsEmailAvailableAsync(string email);
        Task<UserDto> GetUserAsync(UserDto userDto);
        Task<UsersDto> GetUsersAsync();
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(Guid id);
        Task CreateUserAsync(UserDto userDto);
    }
}
