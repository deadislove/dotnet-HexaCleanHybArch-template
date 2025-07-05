namespace Adapters.User.Application.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
    }
}
