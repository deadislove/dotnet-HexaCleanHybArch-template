using Adapters.Auth.Application.Interfaces;
using Adapters.User.Application.DTOs;
using Adapters.User.Application.Interfaces.Facades;
using AutoMapper;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;
using HexaCleanHybArch.Template.Core.DTOs.Response.Auth;
using HexaCleanHybArch.Template.Core.Exceptions;
using HexaCleanHybArch.Template.Core.Interfaces.Auth;
using HexaCleanHybArch.Template.Shared.Auth;

namespace HexaCleanHybArch.Template.Core.Services.Auth
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserFacade _userFacade;
        private readonly IMapper _mapper;

        public JwtAuthService(IJwtTokenService jwtTokenService, IUserFacade userFacade, IMapper mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userFacade = userFacade;
            _mapper = mapper;
        }

        public async Task<LoginCoreResponse?> Login(LoginCoreRequest loginCoreRequest)
        {
            UserDto existingUserDto = await _userFacade.ValidateUserAsync(loginCoreRequest.Email, loginCoreRequest.Password) ?? throw AuthException.InvalidCredentials();
            JwtUserInfo jwtUserInfo = _mapper.Map<JwtUserInfo>(existingUserDto);

            string token = _jwtTokenService.GenerateToken(jwtUserInfo);
            string refreshToken = _jwtTokenService.GenerateRefreshToken(jwtUserInfo);

            return new LoginCoreResponse { 
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshTokenCoreResponse> RefreshAccessToken(RefreshTokenCoreRequest refreshTokenCoreRequest)
        {
            if (!_jwtTokenService.IsRefreshToken(refreshTokenCoreRequest.RefreshToken)) throw AuthException.RefreshTokenInvalid();

            JwtUserInfo jwtUserInfo = _jwtTokenService.ValidateToken(refreshTokenCoreRequest.RefreshToken) ?? throw AuthException.TokenTampered();

            bool isEmailAvailable = await _userFacade.IsEmailAvailableAsync(jwtUserInfo.Email);

            if (isEmailAvailable) throw new NotImplementedException();

            string newAccessToken = _jwtTokenService.GenerateToken(jwtUserInfo);
            string newRefreshToken = _jwtTokenService.GenerateRefreshToken(jwtUserInfo);

            return new RefreshTokenCoreResponse { 
                Token=newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<RegisterCoreResponse?> Register(RegisterCoreRequest registerCoreRequest)
        {
            UserDto userDto = _mapper.Map<UserDto>(registerCoreRequest);
            bool isEmailAvailable = await _userFacade.IsEmailAvailableAsync(userDto.Email);

            if (!isEmailAvailable) throw AuthException.EmailAlreadyExists(userDto.Email);

            await _userFacade.RegisterUserAsync(userDto);

            return new RegisterCoreResponse { 
                Email = userDto.Email,
                Message = "Registration successful"
            };
        }
    }
}
