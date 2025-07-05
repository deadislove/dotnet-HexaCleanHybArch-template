using Adapters.User.Application.DTOs;
using Adapters.User.Application.Interfaces.Facades;
using AutoMapper;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Users;
using HexaCleanHybArch.Template.Core.DTOs.Response.Users;
using HexaCleanHybArch.Template.Core.Mapping;
using HexaCleanHybArch.Template.Core.Services.Users;
using Moq;

namespace HexaCleanHybArch.Template.Tests.Unit.Users
{
    public class UserServiceTests
    {
        private readonly Mock<IUserFacade> _mockUserFacade;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserServiceTests() {
            _mockUserFacade = new Mock<IUserFacade>();

            var mappingConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<CoreMappingProfile>();
            });

            _mapper = mappingConfig.CreateMapper();

            _userService = new UserService(_mockUserFacade.Object, _mapper);
        }

        [Fact]
        public async Task GetUsers()
        {
            UsersCoreResponse mockdata = new UsersCoreResponse { 
                items = new List<UserDto>() {
                    new UserDto { 
                        Id = Guid.NewGuid(),
                        UserName = "Test",
                        Email = "Test@example.com",
                        Password = "123",
                        Profile = null
                    }
                }
            };

            _mockUserFacade.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(mockdata);

            UsersCoreResponse users = await _userService.GetUsersAsync();

            Assert.NotNull(users);
            Assert.IsType<UsersCoreResponse>(users);
            Assert.True(users.items.Any());
        }

        [Fact]
        public async Task GetUser()
        {
            UserCoreRequest userCoreRequest = new UserCoreRequest { 
                Id = Guid.NewGuid(),
            };

            UserCoreResponse mockData = new UserCoreResponse()
            {
                Id = userCoreRequest.Id,
                UserName = "Test",
                Email = "Test@example.com",
                Password = "123",
                Profile = null
            };

            _mockUserFacade.Setup(x => x.GetUserAsync(userCoreRequest))
                .ReturnsAsync(mockData);

            UserCoreResponse user = await _userService.GetUserAsync(userCoreRequest);

            Assert.NotNull(user);
            Assert.IsType<UserCoreResponse>(user);
        }

        [Fact]
        public async Task CreateUser()
        {
            UserCoreRequest userCoreRequest = new UserCoreRequest
            {
                Id = Guid.NewGuid(),
                UserName = "Test",
                Email = "Test@example.com",
                Password = "123",
                Profile = null
            };

            _mockUserFacade.Setup(x => x.CreateUserAsync(userCoreRequest))
                .Returns(Task.CompletedTask);

            await _userService.CreateUserAsync(userCoreRequest);

            _mockUserFacade.Verify(x => x.CreateUserAsync(userCoreRequest), Times.Once);

            Exception? ex = await Record.ExceptionAsync(async () => await _userService.CreateUserAsync(userCoreRequest));
            Assert.Null(ex);
        }

        [Fact]
        public async Task UpdateUser()
        {
            UserCoreRequest userCoreRequest = new UserCoreRequest
            {
                Id = Guid.NewGuid(),
                UserName = "Test",
                Email = "Test@example.com",
                Password = "123",
                Profile = null
            };

            _mockUserFacade.Setup(x => x.UpdateUserAsync(userCoreRequest))
                .Returns(Task.CompletedTask);

            await _userService.UpdateUserAsync(userCoreRequest);

            _mockUserFacade.Verify(x => x.UpdateUserAsync(userCoreRequest), Times.Once);

            Exception? ex = await Record.ExceptionAsync(async () => await _userService.UpdateUserAsync(userCoreRequest));
            Assert.Null(ex);
        }

        [Fact]
        public async Task DeleteUser()
        {
            Guid userId = Guid.NewGuid();

            _mockUserFacade.Setup(x => x.DeleteUserAsync(userId))
                .Returns(Task.CompletedTask);

            await _userService.DeleteUserAsync(userId);

            _mockUserFacade.Verify(x => x.DeleteUserAsync(userId), Times.Once);

            Exception? ex = await Record.ExceptionAsync(async () => await _userService.DeleteUserAsync(userId));
            Assert.Null(ex);
        }
    }
}
