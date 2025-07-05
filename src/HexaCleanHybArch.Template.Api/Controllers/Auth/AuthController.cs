using FluentValidation.Results;
using HexaCleanHybArch.Template.Api.Validator;
using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;
using HexaCleanHybArch.Template.Core.DTOs.Response.Auth;
using HexaCleanHybArch.Template.Core.Interfaces.Auth;
using HexaCleanHybArch.Template.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HexaCleanHybArch.Template.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthService _jwtAuthService;

        public AuthController(IJwtAuthService jwtAuthService) { 
            _jwtAuthService = jwtAuthService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterCoreRequest requestModel) {

            RegisterValidator validator = new RegisterValidator();
            ValidationResult validationResult = await validator.ValidateAsync(requestModel);

            if (!validationResult.IsValid) {
                return BadRequest(ResponseHelper.BadRequest(validationResult.Errors));
            }

            RegisterCoreResponse? response = await _jwtAuthService.Register(requestModel);

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(LoginCoreRequest requesstModel) {

            LoginValidator validator = new LoginValidator();
            ValidationResult validationResult = await validator.ValidateAsync(requesstModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(ResponseHelper.BadRequest(validationResult.Errors));
            }

            LoginCoreResponse? response = await _jwtAuthService.Login(requesstModel);

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshTokenUser(RefreshTokenCoreRequest requestModel)
        {
            RefreshTokenValidator validator = new RefreshTokenValidator();
            ValidationResult validationResult = await validator.ValidateAsync(requestModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(ResponseHelper.BadRequest(validationResult.Errors));
            }

            RefreshTokenCoreResponse response = await _jwtAuthService.RefreshAccessToken(requestModel);

            return Ok(ResponseHelper.Success(response));
        }
    }
}
