using Adapters.User.Application.DTOs;
using Adapters.User.Application.Exceptions;
using Adapters.User.Application.Interfaces;
using Adapters.User.Application.Interfaces.Facades;
using AutoMapper;

namespace Adapters.User.Application.Facades
{
    public class UserFacade: IUserFacade
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserFacade(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task RegisterUserAsync(UserDto userDto)
        {
            if (!await IsEmailAvailableAsync(userDto.Email))
                throw new UserAlreadyExistsException(userDto.Email);

            await _userService.CreateUserAsync(userDto);
        }

        public async Task<UserDto?> ValidateUserAsync(string email, string password = "")
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            // Try to get user by email only
            UserDto? user = await _userService.GetUserByEmailAsync(email);

            // If found, verify password
            if (user != null && BCrypt.Net.BCrypt.Verify(password.Trim(), user.Password))
                return user;

            return null;
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            UserDto? user = await _userService.GetUserByEmailAsync(email);
            return user == null;
        }

        // CRUD User
        public async Task<UsersDto> GetUsersAsync()
        {
            IEnumerable<UserDto>? users = await _userService.GetAllUsersAsync();

            return new UsersDto {
                items = users,
            };
        }

        public async Task<UserDto> GetUserAsync(UserDto userDto)
        {
            UserDto? user = null;

            if (!string.IsNullOrEmpty(userDto.Email))
            {
                user = await _userService.GetUserByEmailAsync(userDto.Email);
            }

            if (userDto.Id != Guid.Empty) { 
                user = await _userService.GetUserByIdAsync(userDto.Id);
            }

            return user ?? new UserDto();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            if (userDto == null) return;

            await _userService.UpdateUserAsync(userDto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            await _userService.CreateUserAsync(userDto);
        }
    }
}
