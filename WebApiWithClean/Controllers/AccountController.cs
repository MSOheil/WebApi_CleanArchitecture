using Application.Common.Interfaces;
using Application.Common.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithClean.Controllers
{
    [ApiController]
    [Route("api/accountcontroller")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [Route("/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            var result = await _identityService.RegisterAsync(model);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    return BadRequest(err.Description);
                }
            }
            return Ok();
        }
        [Route("/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            var result = await _identityService.LoginAsync(model);
            if (result == null)
            {
                return BadRequest($"Not User With this email :{model.Email} ");
            }
            return Ok(new { jwttoken = result });
            
        }
        [Route("ForgetPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync([FromQuery] string userEmail)
        {
            var token = await _identityService.ForgetPasswordtokenAsync(userEmail);
            if (token is null)
            {
                return BadRequest("Not User With This Email");
            }
            return Ok(token);
        }
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto model)
        {
            var result = await _identityService.ResetPasswordAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest("This User Not Exist");
            }
            return Ok();
        }

    }
}
