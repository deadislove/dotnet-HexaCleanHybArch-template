using Adapters.User.Domain.DTOs.Bases;

namespace Adapters.User.Domain.DTOs
{
    public class UserProfile: BaseDomain
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = default!;
        public DateTime Birthday { get; set; }

        public User? User { get; set; }
    }
}
