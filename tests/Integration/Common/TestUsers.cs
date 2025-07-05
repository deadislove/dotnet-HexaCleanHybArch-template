using Adapters.Auth.Application.DTOs.RequestModel;
using Adapters.User.Application.DTOs;

namespace HexaCleanHybArch.Template.Tests.Integration.Common
{
    public static class TestUsers
    {
        public static readonly string TestGuid = "644fb447-0a6f-4c19-8c05-78337d84eb41";

        public static readonly UserDto TestUser = new UserDto
        {
            Id = Guid.Parse(TestGuid),
            UserName = "Test User",
            Email = "test@yahoo.com",
            Password = "123456"
        };

        public static readonly UserDto SeedUser = new UserDto
        {
            UserName = "testuser",
            Email = "test@example.com",
            Password = "123456"
        };

        public static readonly RegisterDto RegisterDto = new RegisterDto
        {
            UserName = "Test User",
            Email = "test@yahoo.com",
            Password = "123456"
        };
    }
}
