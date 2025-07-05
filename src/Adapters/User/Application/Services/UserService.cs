using Adapters.User.Application.DTOs;
using Adapters.User.Application.Interfaces;
using Adapters.User.Domain.Interfaces;
using AutoMapper;
using Users = Adapters.User.Domain.DTOs.User;

namespace Adapters.User.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            Users user = _mapper.Map<Users>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.AddAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            IEnumerable<Users>? users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            Users? user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            Users? user = await _userRepository.GetByEmailAsync(email);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            Users? user = await _userRepository.GetByEmailAndPasswordAsync(email, password);

            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            Users user = _mapper.Map<Users>(userDto);
            await _userRepository.UpdateAsync(user);
        }
    }
}
