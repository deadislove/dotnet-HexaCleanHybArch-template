using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Users;
using HexaCleanHybArch.Template.Core.DTOs.Response.Users;
using HexaCleanHybArch.Template.Core.Interfaces.Users;
using HexaCleanHybArch.Template.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HexaCleanHybArch.Template.Api.Controllers.Users
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices) {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUser() {

            UsersCoreResponse response = await _userServices.GetUsersAsync();

            return Ok(ResponseHelper.Success(response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            if (id == Guid.Empty) {
                return BadRequest(ResponseHelper.BadRequest("ID is empty."));
            }

            UserCoreRequest request = new UserCoreRequest() { Id = id };

            UserCoreResponse response = await _userServices.GetUserAsync(request);

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCoreRequest request)
        {
            await _userServices.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUser), new { id = request.Id }, ResponseHelper.Success(request, 201));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserCoreRequest request)
        {
            if (id != request.Id || id == Guid.Empty || request.Id == Guid.Empty)
            {
                return BadRequest(ResponseHelper.BadRequest("Query Parameter doesn't match the body property value"));
            }

            await _userServices.UpdateUserAsync(request);

            return Ok(ResponseHelper.NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userServices.DeleteUserAsync(id);
            return Ok(ResponseHelper.NoContent());
        }
    }
}
