namespace Adapters.User.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserProfileDto? Profile { get; set; }
    }

    public class UsersDto
    {
        public IEnumerable<UserDto> items {  get; set; } = new List<UserDto>();
    }
}
