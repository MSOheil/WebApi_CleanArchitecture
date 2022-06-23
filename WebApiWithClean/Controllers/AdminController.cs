using Application.Common.Interfaces;
using Application.Common.Models.Dtos;
using Infrastructure.Identity.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiWithClean.Controllers
{

    [ApiController]
    [Authorize(Policy = "Role")]
    [Route("api/admincontroller")]
    public class AdminController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public AdminController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [Route("/ListUsers")]
        [HttpGet]
        public IActionResult ListUser()
        {
            var users = _identityService.ListUsers();
            return Ok(users);
        }
        [Route("/EditUser")]
        [HttpPut]
        public async Task<IActionResult> EditUserAsync([FromQuery] string email, [FromBody] EditUserDto model)
        {
            var result = await _identityService.EditUserAsync(email, model);
            if (!result.Succeeded)
            {
                return BadRequest("This Usert Not Exist");
            }
            return Ok();
        }
        [Route("/DeleteUser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string userId)
        {
            var result = await _identityService.DeleteUserAsync(userId);
            if (!result.Succeeded)
            {
                return BadRequest("This User Not Exist");
            }
            return Ok();
        }
        [Route("/ListRoles")]
        [HttpGet]
        public IActionResult ListRoles()
        {
            var result = _identityService.ListRoles();
            return Ok(result);
        }
        [Route("/CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAync([FromQuery] string roleName)
        {
            var result = await _identityService.CreaRoleAsync(roleName);
            if (!result.Succeeded)
            {
                return BadRequest("Pleas Write role");
            }
            return Ok();
        }
        [Route("/EditRole")]
        [HttpPost]
        public async Task<IActionResult> EditRoleAsync([FromBody] EditRoleDto model)
        {
            var result = await _identityService.UpdateRoleAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest($"Not Exist This {model.RoleName}");
            }
            return Ok();
        }
        [Route("/AddUserToRole")]
        [HttpPost]
        public async Task<IActionResult> AddUserToRoleAsync([FromQuery] string userId, [FromBody] List<UserRolesDto> model)
        {
            var result = await _identityService.AddUserToRoleAsync(userId, model);
            if (!result.Succeeded)
            {
                return BadRequest($"Not Exist This {userId}");
            }
            return Ok();
        }
        [Route("/DeleteUserInRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserInRole([FromQuery] string userId, [FromBody] List<UserRolesDto> model)
        {
            var result = await _identityService.RemoveUserFromRolesAsync(userId, model);
            if (!result.Succeeded)
            {
                return BadRequest($"Not Exist This {userId}");
            }
            return Ok();
        }
        [Route("/GetClaimsUser")]
        [HttpGet]
        public async Task<IActionResult> GetClaimsUser([FromQuery] string userId)
        {
            var result = await _identityService.GetUserClaims(userId);
            if (result == null)
            {
                return BadRequest($"This User With Id{userId} Not Exist");
            }
            return Ok(result);
        }
        [Route("/ManageUserClaims")]
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims([FromQuery] string userId, [FromBody] List<UserClaims> claim)
        {
            var result = await _identityService.ManageUserClaims(userId, claim);
            if (!result.Succeeded)
            {
                return BadRequest($"This User With Id{userId} Has This Claim");
            }
            return Ok();
        }
        [HttpPost("userId")]
        public async Task<IActionResult> RemoveClaimsUser([FromQuery] string userId)
        {
            var result = await _identityService.DeleteUserClaims(userId);
            if (!result.Succeeded)
            {
                return BadRequest($"This User With Id{userId} Not Exists");
            }
            return Ok();
        }
    }
}
