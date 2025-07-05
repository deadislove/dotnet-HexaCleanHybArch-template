using Adapters.Auth.Application.Interfaces;
using Adapters.User.Application.DTOs;
using Adapters.User.Application.Interfaces.Facades;
using AutoMapper;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;
using HexaCleanHybArch.Template.Core.DTOs.Response.Auth;
using HexaCleanHybArch.Template.Core.Services.Auth;
using HexaCleanHybArch.Template.Shared.Auth;
using Moq;

namespace HexaCleanHybArch.Template.Tests.Unit.Auth
{
    public class AuthServiceTest
    {
        private readonly Mock<IJwtTokenService> _mockJwtTokenService;
        private readonly Mock<IUserFacade> _mockUserFacade;
        private readonly IMapper _mapper;
        private readonly JwtAuthService _jwtAuthService;

        public AuthServiceTest() { 
            _mockJwtTokenService = new Mock<IJwtTokenService>();
            _mockUserFacade = new Mock<IUserFacade>();

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDto, JwtUserInfo>();
                cfg.CreateMap<RegisterCoreRequest, UserDto>();
            });

            _mapper = config.CreateMapper();

            _jwtAuthService = new JwtAuthService(_mockJwtTokenService.Object, _mockUserFacade.Object, _mapper);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            LoginCoreRequest loginRequest = new LoginCoreRequest
            {
                Email = "test@example.com",
                Password = "123456"
            };

            UserDto userDto = new UserDto
            {
                Email = loginRequest.Email,
                UserName = "Test User"
            };

            _mockUserFacade.Setup(x => x.ValidateUserAsync(loginRequest.Email, loginRequest.Password))
                           .ReturnsAsync(userDto);

            _mockJwtTokenService.Setup(x => x.GenerateToken(It.IsAny<JwtUserInfo>()))
                                .Returns("fake_token");

            _mockJwtTokenService.Setup(x => x.GenerateRefreshToken(It.IsAny<JwtUserInfo>()))
                                .Returns("fake_refresh_token");

            // Act
            LoginCoreResponse? result = await _jwtAuthService.Login(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("fake_token", result.Token);
            Assert.Equal("fake_refresh_token", result.RefreshToken);
        }
                
    }
}
